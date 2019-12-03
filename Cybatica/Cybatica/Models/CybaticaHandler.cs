using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Cybatica.Empatica;
using Cybatica.Services;
using Cybatica.Utilities;
using DynamicData;
using DynamicData.Aggregation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Essentials;

namespace Cybatica.Models
{
    public enum SessionType
    {
        None = 0,
        Base = 1,
        Data = 2
    }

    public class CybaticaHandler : ReactiveObject, ICybaticaHandler
    {
        private readonly IDisposable _cleanUp;
        private readonly IDataExporter _dataExporter;
        private readonly IEmpaticaHandler _empaticaHandler;
        private readonly EmpaticaSession _empaticaSession;
        private readonly OcsSession _ocsSession;
        private readonly Stopwatch _stopwatch;

        private bool _isCapturing;
        private bool _isDataSession;
        private double _startedTime;
        private IDisposable _stopwatchDisposable;

        private float _baseNnMeanAve;
        private float _baseSdNnAve;
        private float _baseMeanEdaAve;
        private float _basePeakEdaAve;

        public CybaticaHandler(IEmpaticaHandler empaticaHandler = null, IDataExporter dataExporter = null)
        {
            _empaticaHandler = empaticaHandler ?? Locator.Current.GetService<IEmpaticaHandler>();
            _dataExporter = dataExporter ?? Locator.Current.GetService<IDataExporter>();

            _empaticaSession = new EmpaticaSession();
            _ocsSession = new OcsSession();
            _stopwatch = new Stopwatch();

            var calculator = new OcsCalculator();

            AccelerationConnectable = _empaticaSession.Acceleration.Connect();
            BatteryConnectable = _empaticaSession.BatteryLevel.Connect();
            BvpConnectable = _empaticaSession.Bvp.Connect();
            GsrConnectable = _empaticaSession.Gsr.Connect();
            HrConnectable = _empaticaSession.Hr.Connect();
            IbiConnectable = _empaticaSession.Ibi.Connect();
            TagConnectable = _empaticaSession.Tag.Connect();
            TemperatureConnectable = _empaticaSession.Temperature.Connect();

            OcsConnectable = _ocsSession.Ocs.Connect();
            NnMeanConnectable = _ocsSession.NnMean.Connect();
            SdNnConnectable = _ocsSession.SdNn.Connect();
            MeanEdaConnectable = _ocsSession.MeanEda.Connect();
            PeakEdaConnectable = _ocsSession.PeakEda.Connect();

            _empaticaHandler.AccelerationAction = acceleration =>
            {
                _empaticaSession.AddAcceleration(acceleration);
                Acceleration = acceleration;
            };

            _empaticaHandler.BvpAction = bvp =>
            {
                _empaticaSession.AddBvp(bvp);
                Bvp = bvp;
            };

            _empaticaHandler.GsrAction = gsr =>
            {
                _empaticaSession.AddGsr(gsr);
                Gsr = gsr;
            };

            _empaticaHandler.HrAction = hr =>
            {
                _empaticaSession.AddHr(hr);
                Hr = hr;
            };

            _empaticaHandler.IbiAction = ibi =>
            {
                _empaticaSession.AddIbi(ibi);
                Ibi = ibi;
            };

            _empaticaHandler.TemperatureAction = temperature =>
            {
                _empaticaSession.AddTemperature(temperature);
                Temperature = temperature;
            };

            var ocs = Observable.Interval(TimeSpan.FromSeconds(1))
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Where(_ => _isDataSession)
                .Sample(TimeSpan.FromSeconds(1))
                .Where(_ => _isCapturing)
                .Subscribe(time =>
                {
                    var nnMeanRatio = _ocsSession.NnMean.Items.LastOrDefault().Value / _baseNnMeanAve;
                    var sdNnRatio = _ocsSession.SdNn.Items.LastOrDefault().Value / _baseSdNnAve;
                    var meanEdaRatio = _ocsSession.MeanEda.Items.LastOrDefault().Value / _baseMeanEdaAve;
                    var peakEdaRatio = _ocsSession.PeakEda.Items.LastOrDefault().Value / _basePeakEdaAve;
                    var calculatedOcs =
                        new AnalysisData(calculator.CalculateOcs(nnMeanRatio, sdNnRatio,
                                meanEdaRatio, peakEdaRatio), time);
                    _ocsSession.AddOcs(calculatedOcs);
                    Ocs = calculatedOcs;
                });

            var filteredIbi = IbiConnectable
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .Filter(_ => _isCapturing)
                .Filter(x => x.Timestamp > DateTimeOffset.Now.ToUnixTimeSeconds() - _startedTime - 60);

            var nnMean = filteredIbi
                .Avg(x => x.Value)
                .Subscribe(x =>
                {
                    var data = new AnalysisData(x, DateTimeOffset.Now.ToUnixTimeSeconds() - _startedTime);
                    _ocsSession.AddNnMean(data);
                    NnMean = data;
                });

            var sdNn = filteredIbi
                .StdDev(x => x.Value)
                .Subscribe(x =>
                {
                    var data = new AnalysisData((float)x, DateTimeOffset.Now.ToUnixTimeSeconds() - _startedTime);
                    _ocsSession.AddSdNn(data);
                    SdNn = data;
                });

            var filteredGsr = GsrConnectable
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .Filter(_ => _isCapturing)
                .Filter(x => x.Timestamp > DateTimeOffset.Now.ToUnixTimeSeconds() - _startedTime - 60);

            var meanEda = filteredGsr
                .Avg(x => x.Value)
                .Subscribe(x =>
                {
                    var data = new AnalysisData(x, DateTimeOffset.Now.ToUnixTimeSeconds() - _startedTime);
                    _ocsSession.AddMeanEda(data);
                    MeanEda = data;
                });

            var peakEda = filteredGsr
                .Maximum(x => x.Value)
                .Subscribe(x =>
                {
                    var data = new AnalysisData(x, DateTimeOffset.Now.ToUnixTimeSeconds() - _startedTime);
                    _ocsSession.AddPeakEda(data);
                    PeakEda = data;
                });

            _cleanUp = new CompositeDisposable(ocs, nnMean, sdNn, meanEda, peakEda);
        }

        private void ConfigureBaseData()
        {
            _baseNnMeanAve = _ocsSession.NnMean.Items.Where(x => 60 < x.Timestamp).Average(x => x.Value);
            _baseSdNnAve = _ocsSession.SdNn.Items.Where(x => 60 < x.Timestamp).Average(x => x.Value);
            _baseMeanEdaAve = _ocsSession.MeanEda.Items.Where(x => 60 < x.Timestamp).Average(x => x.Value);
            _basePeakEdaAve = _ocsSession.PeakEda.Items.Where(x => 60 < x.Timestamp).Average(x => x.Value);
        }

        #region IDisposable

        public void Dispose()
        {
            _cleanUp?.Dispose();
            _stopwatchDisposable?.Dispose();
            AccelerationConnectable?.DisposeMany();
            BatteryConnectable?.DisposeMany();
            BvpConnectable?.DisposeMany();
            GsrConnectable?.DisposeMany();
            IbiConnectable?.DisposeMany();
            HrConnectable?.DisposeMany();
            TemperatureConnectable?.DisposeMany();
            OcsConnectable?.DisposeMany();
            NnMeanConnectable?.DisposeMany();
            SdNnConnectable?.DisposeMany();
            MeanEdaConnectable?.DisposeMany();
            PeakEdaConnectable?.DisposeMany();
        }

        #endregion

        #region ICybaticaHandler

        public void InitializeSession()
        {
            _empaticaSession.InitializeSession();
            _ocsSession.InitializeSession();
        }

        public void Connect(EmpaticaDevice device)
        {
            _empaticaHandler.Connect(device);
        }

        public void Disconnect()
        {
            _empaticaHandler.Disconnect();
        }

        public void Discover()
        {
            _empaticaHandler.Discover();
        }

        public void StartSession(SessionType sessionType)
        {
            InitializeSession();

            _startedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            _empaticaHandler.StartSession(_startedTime);

            CurrentSessionType = sessionType;

            _isDataSession = CurrentSessionType.Equals(SessionType.Data);

            _stopwatch.Reset();
            _stopwatch.Start();
            _stopwatchDisposable = Observable.Interval(TimeSpan.FromSeconds(0.01))
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ => ElapsedTime = _stopwatch.Elapsed);

            _isCapturing = true;
        }

        public void StopSession()
        {
            if (CurrentSessionType.Equals(SessionType.Base))
            {
                ConfigureBaseData();
            }

            CurrentSessionType = SessionType.None;
            _empaticaHandler.StopSession();
            _stopwatch.Stop();
            _stopwatchDisposable?.Dispose();
            _stopwatchDisposable = null;
            _isCapturing = false;
        }

        public IEnumerable<EmpaticaDevice> GetDiscoveredDevices()
        {
            return _empaticaHandler.GetDiscoveredDevices();
        }

        public void Export()
        {
            _dataExporter.Export(FileSystem.CacheDirectory, _empaticaSession, _ocsSession);
        }

        [Reactive] public TimeSpan ElapsedTime { get; private set; }

        public SessionType CurrentSessionType { get; private set; }

        #endregion

        #region IEmpaticaModel

        [Reactive] public Acceleration Acceleration { get; private set; }
        [Reactive] public BatteryLevel BatteryLevel { get; private set; }
        [Reactive] public Bvp Bvp { get; private set; }
        [Reactive] public Gsr Gsr { get; private set; }
        [Reactive] public Hr Hr { get; private set; }
        [Reactive] public Ibi Ibi { get; private set; }
        [Reactive] public Tag Tag { get; private set; }
        [Reactive] public Temperature Temperature { get; private set; }

        #endregion

        #region IEmpaticaSessionConnector

        public IObservable<IChangeSet<Acceleration>> AccelerationConnectable { get; }
        public IObservable<IChangeSet<BatteryLevel>> BatteryConnectable { get; }
        public IObservable<IChangeSet<Bvp>> BvpConnectable { get; }
        public IObservable<IChangeSet<Gsr>> GsrConnectable { get; }
        public IObservable<IChangeSet<Hr>> HrConnectable { get; }
        public IObservable<IChangeSet<Ibi>> IbiConnectable { get; }
        public IObservable<IChangeSet<Tag>> TagConnectable { get; }
        public IObservable<IChangeSet<Temperature>> TemperatureConnectable { get; }

        #endregion

        #region IOcsModel

        [Reactive] public AnalysisData Ocs { get; private set; }
        [Reactive] public AnalysisData NnMean { get; private set; }
        [Reactive] public AnalysisData SdNn { get; private set; }
        [Reactive] public AnalysisData MeanEda { get; private set; }
        [Reactive] public AnalysisData PeakEda { get; private set; }

        #endregion

        #region IOcsSessionConnector

        public IObservable<IChangeSet<AnalysisData>> OcsConnectable { get; }
        public IObservable<IChangeSet<AnalysisData>> NnMeanConnectable { get; }
        public IObservable<IChangeSet<AnalysisData>> SdNnConnectable { get; }
        public IObservable<IChangeSet<AnalysisData>> MeanEdaConnectable { get; }
        public IObservable<IChangeSet<AnalysisData>> PeakEdaConnectable { get; }

        #endregion
    }
}