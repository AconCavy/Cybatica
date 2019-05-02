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
    public class EmpaticaChartViewModel : ReactiveObject, ISupportsActivation
    {
        public ReadOnlyObservableCollection<BVP> BVP => _bvp;
        public ReadOnlyObservableCollection<IBI> IBI => _ibi;
        public ReadOnlyObservableCollection<HR> HR => _hr;
        public ReadOnlyObservableCollection<GSR> GSR => _gsr;
        public ReadOnlyObservableCollection<Temperature> Temperature => _temperature;

        private readonly ReadOnlyObservableCollection<BVP> _bvp;
        private readonly ReadOnlyObservableCollection<IBI> _ibi;
        private readonly ReadOnlyObservableCollection<HR> _hr;
        private readonly ReadOnlyObservableCollection<GSR> _gsr;
        private readonly ReadOnlyObservableCollection<Temperature> _temperature;
        
        private readonly EmpaticaSession _empaticaSession;

        public ViewModelActivator Activator { get; }

        public EmpaticaChartViewModel()
        {
            Activator = new ViewModelActivator();

            _empaticaSession = Locator.Current.GetService<IEmpaticaHandler>()
                .DeviceDelegate.EmpaticaSession;

            _empaticaSession.BVP.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _bvp)
                .Subscribe();

            _empaticaSession.IBI.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ibi)
                .Subscribe();

            _empaticaSession.HR.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _hr)
                .Subscribe();

            _empaticaSession.GSR.Connect()
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
            Console.WriteLine("EmpaticaChartViewModel: Activated");

        }

        private void HandleDeactivation()
        {
            Console.WriteLine("EmpaticaChartViewModel: Deactivated");

        }

    }

}
