using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Cybatica.Empatica;
using Cybatica.Services;
using Cybatica.Utilities;
using DynamicData;
using ReactiveUI;
using Splat;

namespace Cybatica.Models
{
    public class CybaticaHandler : ReactiveObject, ICybaticaHandler, IDisposable
    {
        private readonly IEmpaticaHandler _empaticaHandler;
        private readonly ReadOnlyObservableCollection<Gsr> _gsr;
        private readonly ReadOnlyObservableCollection<Ibi> _ibi;
        private readonly IObservable<long> _ocsObservable;

        private bool _isCapturing;
        private IDisposable _oCsDisposable;
        private double _startedTime;

        public CybaticaHandler()
        {
            _empaticaHandler = Locator.Current.GetService<IEmpaticaHandler>();
            EmpaticaSession = new EmpaticaSession();
            OcsSession = new OcsSession();
            BioDataModel = new BioDataModel();
            OcsModel = new OcsModel();

            _empaticaHandler.BvpAction = bvp =>
            {
                EmpaticaSession.Bvp.Add(bvp);
                BioDataModel.Bvp = bvp;
            };

            _empaticaHandler.IbiAction = ibi =>
            {
                EmpaticaSession.Ibi.Add(ibi);
                BioDataModel.Ibi = ibi;
            };

            _empaticaHandler.HrAction = hr =>
            {
                EmpaticaSession.Hr.Add(hr);
                BioDataModel.Hr = hr;
            };

            _empaticaHandler.GsrAction = gsr =>
            {
                EmpaticaSession.Gsr.Add(gsr);
                BioDataModel.Gsr = gsr;
            };

            _empaticaHandler.TemperatureAction = temp =>
            {
                EmpaticaSession.Temperature.Add(temp);
                BioDataModel.Temperature = temp;
            };

            _empaticaHandler.AccelerationAction = acc =>
            {
                EmpaticaSession.Acceleration.Add(acc);
                BioDataModel.Acceleration = acc;
            };

            EmpaticaSession.Ibi.Connect()
                .Filter(_ => _isCapturing)
                //.Filter(x => x.Timestamp > DateTimeOffset.Now.ToUnixTimeSeconds() - 60)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ibi)
                .Subscribe();

            EmpaticaSession.Gsr.Connect()
                .Filter(_ => _isCapturing)
                //.Filter(x => x.Timestamp > DateTimeOffset.Now.ToUnixTimeSeconds() - 60)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _gsr)
                .Subscribe();

            _ocsObservable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Do(_ =>
                {
                    var time = DateTimeOffset.Now.ToUnixTimeSeconds() - _startedTime;
                    var oCs = Calculator.CalcOcs();
                    OcsSession.Ocs.Add(new AnalysisData(oCs, time));
                    OcsModel.Ocs = oCs;

                    var nnMean = Calculator.CalcNnMean(_ibi);
                    OcsSession.NnMean.Add(new AnalysisData(nnMean, time));
                    OcsModel.NnMean = nnMean;

                    var sdNn = Calculator.CalcSdNn(_ibi);
                    OcsSession.SdNn.Add(new AnalysisData(sdNn, time));
                    OcsModel.SdNn = sdNn;

                    var meanEda = Calculator.CalcMeanEda(_gsr);
                    OcsSession.MeanEda.Add(new AnalysisData(meanEda, time));
                    OcsModel.MeanEda = meanEda;

                    var peakEda = Calculator.CalcPeakEda(_gsr);
                    OcsSession.PeakEda.Add(new AnalysisData(peakEda, time));
                    OcsModel.PeakEda = peakEda;
                });
        }

        public EmpaticaSession EmpaticaSession { get; }

        public OcsSession OcsSession { get; }

        public ReadOnlyCollection<EmpaticaDevice> Devices => _empaticaHandler.Devices;

        public BioDataModel BioDataModel { get; set; }

        public OcsModel OcsModel { get; set; }

        public void InitializeSession()
        {
            EmpaticaSession.InitializeSession();
            OcsSession.InitializeSession();
        }

        public void Connect(EmpaticaDevice device)
        {
            _empaticaHandler.Connect(device);
        }

        public void Disconnect()
        {
            _empaticaHandler.Disconnect();
        }

        public void StartSession()
        {
            InitializeSession();

            _startedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            _empaticaHandler.StartSession(_startedTime);

            _oCsDisposable = _ocsObservable.Subscribe();
            _isCapturing = true;
        }

        public void StopSession()
        {
            _empaticaHandler.StopSession();

            _oCsDisposable?.Dispose();
            _oCsDisposable = null;
            _isCapturing = false;
        }

        public void Dispose()
        {
            _oCsDisposable?.Dispose();
        }
    }
}