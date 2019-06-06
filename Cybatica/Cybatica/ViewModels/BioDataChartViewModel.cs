using Cybatica.Empatica;
using Cybatica.Services;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Cybatica.ViewModels
{
    public class BioDataChartViewModel : ReactiveObject, ISupportsActivation
    {
        public ReadOnlyObservableCollection<Bvp> Bvp => _bvp;
        public ReadOnlyObservableCollection<Ibi> Ibi => _ibi;
        public ReadOnlyObservableCollection<Hr> Hr => _hr;
        public ReadOnlyObservableCollection<Gsr> Gsr => _gsr;
        public ReadOnlyObservableCollection<Temperature> Temperature => _temperature;

        private readonly ReadOnlyObservableCollection<Bvp> _bvp;
        private readonly ReadOnlyObservableCollection<Ibi> _ibi;
        private readonly ReadOnlyObservableCollection<Hr> _hr;
        private readonly ReadOnlyObservableCollection<Gsr> _gsr;
        private readonly ReadOnlyObservableCollection<Temperature> _temperature;

        private readonly CybaticaHandler _handler;
        private readonly EmpaticaSession _empaticaSession;

        private readonly IDisposable _observableBvp;
        private readonly IDisposable _observableIbi;
        private readonly IDisposable _observableHr;
        private readonly IDisposable _observableGsr;
        private readonly IDisposable _observableTemperature;

        public ViewModelActivator Activator { get; }

        public BioDataChartViewModel()
        {
            Activator = new ViewModelActivator();

            _handler = new CybaticaHandler();

            _empaticaSession = _handler.EmpaticaSession;

            var bvp = _empaticaSession.ConnectBvp();
            _observableBvp = bvp
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _bvp)
                .Subscribe();

            var ibi = _empaticaSession.ConnectIbi();
            _observableIbi = ibi
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ibi)
                .Subscribe();

            var hr = _empaticaSession.ConnectHr();
            _observableHr = hr
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _hr)
                .Subscribe();

            var gsr = _empaticaSession.ConnectGsr();
            _observableGsr = gsr
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _gsr)
                .Subscribe();

            var temperature = _empaticaSession.ConnectTemperature();
            _observableTemperature = temperature
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _temperature)
                .Subscribe();

            this.WhenActivated(disposable =>
            {
                HandleActivation();
                Disposable.Create(() => HandleDeactivation())
                .DisposeWith(disposable);

            });

        }

        private void HandleActivation()
        {
            
        }

        private void HandleDeactivation()
        {
            _observableBvp.Dispose();
            _observableGsr.Dispose();
            _observableHr.Dispose();
            _observableIbi.Dispose();
            _observableTemperature.Dispose();

        }

    }

}
