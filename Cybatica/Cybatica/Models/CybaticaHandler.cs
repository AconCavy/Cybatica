using Cybatica.Empatica;
using Cybatica.Services;
using Cybatica.Utilities;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Cybatica.Models
{
    public class CybaticaHandler : ReactiveObject, ICybaticaHandler, IDisposable
    {
        public EmpaticaSession EmpaticaSession { get; private set; }

        public OCSSession OCSSession { get; private set; }

        public ReadOnlyCollection<EmpaticaDevice> Devices => _empaticaHandler.Devices;

        public BioDataModel BioDataModel { get; set; }

        public OCSModel OcsModel { get; set; }

        private readonly IEmpaticaHandler _empaticaHandler;
        private readonly IObservable<long> _ocsObservable;
        private IDisposable _ocsDisposable;

        private readonly ReadOnlyObservableCollection<Ibi> _ibi;
        private readonly ReadOnlyObservableCollection<Gsr> _gsr;
        private bool _isCapturing;
        private double _startedTime;

        public CybaticaHandler()
        {
            _empaticaHandler = Locator.Current.GetService<IEmpaticaHandler>();
            EmpaticaSession = new EmpaticaSession();
            OCSSession = new OCSSession();
            BioDataModel = new BioDataModel();
            OcsModel = new OCSModel();

            _empaticaHandler.BvpAction = bvp => {
                EmpaticaSession.Bvp.Add(bvp);
                BioDataModel.Bvp = bvp;
            };

            _empaticaHandler.IbiAction = ibi => {
                EmpaticaSession.Ibi.Add(ibi);
                BioDataModel.Ibi = ibi;
            };

            _empaticaHandler.HrAction = hr =>
            {
                EmpaticaSession.Hr.Add(hr);
                BioDataModel.Hr = hr;
            };

            _empaticaHandler.GsrAction = gsr => {
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
                    var ocs = Calculator.CalcOcs();
                    OCSSession.Ocs.Add(new AnalysisData(ocs, time));
                    OcsModel.Ocs = ocs;
                    
                    var nnMean = Calculator.CalcNnMean(_ibi);
                    OCSSession.NnMean.Add(new AnalysisData(nnMean, time));
                    OcsModel.NnMean = nnMean;

                    var sdNn = Calculator.CalcSdNn(_ibi);
                    OCSSession.SdNn.Add(new AnalysisData(sdNn, time));
                    OcsModel.SdNn = sdNn;

                    var meanEda = Calculator.CalcMeanEda(_gsr);
                    OCSSession.MeanEda.Add(new AnalysisData(meanEda, time));
                    OcsModel.MeanEda = meanEda;

                    var peakEda = Calculator.CalcPeakEda(_gsr);
                    OCSSession.PeakEda.Add(new AnalysisData(peakEda, time));
                    OcsModel.PeakEda = peakEda;
                });
        }

        public void InitializeSession()
        {
            EmpaticaSession.InitializeSession();
            OCSSession.InitializeSession();
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

            _ocsDisposable = _ocsObservable.Subscribe();
            _isCapturing = true;
        }

        public void StopSession()
        {
            _empaticaHandler.StopSession();

            _ocsDisposable?.Dispose();
            _ocsDisposable = null;
            _isCapturing = false;
        }

        public void Dispose()
        {
            _ocsDisposable?.Dispose();
        }
    }
}
