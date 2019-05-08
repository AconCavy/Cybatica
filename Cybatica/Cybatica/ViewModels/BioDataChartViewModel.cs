using Cybatica.Empatica;
using Cybatica.Services;
using DynamicData;
using ReactiveUI;
using Splat;
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
        
        private readonly EmpaticaSession _empaticaSession;

        public ViewModelActivator Activator { get; }

        public BioDataChartViewModel()
        {
            Activator = new ViewModelActivator();

            _empaticaSession = Locator.Current.GetService<IEmpaticaHandler>().EmpaticaSession;

            _empaticaSession.Bvp.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _bvp)
                .Subscribe();

            _empaticaSession.Ibi.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ibi)
                .Subscribe();

            _empaticaSession.Hr.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _hr)
                .Subscribe();

            _empaticaSession.Gsr.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _gsr)
                .Subscribe();

            _empaticaSession.Temperature.Connect()
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
        }

    }

}
