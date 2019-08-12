using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Cybatica.Empatica;
using Cybatica.Services;
using DynamicData;
using ReactiveUI;
using Splat;

namespace Cybatica.ViewModels
{
    public class BioDataChartViewModel : ReactiveObject, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Bvp> _bvp;
        private readonly ReadOnlyObservableCollection<Gsr> _gsr;
        private readonly ReadOnlyObservableCollection<Temperature> _temperature;
        private readonly IDisposable _cleanUp;

        public BioDataChartViewModel()
        {
            var handler = Locator.Current.GetService<ICybaticaHandler>();
            var session = handler.EmpaticaSession;

            var bvp = session.Bvp.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _bvp)
                .Subscribe();

            var gsr = session.Gsr.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _gsr)
                .Subscribe();

            var temperature = session.Temperature.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _temperature)
                .Subscribe();

            _cleanUp = new CompositeDisposable(bvp, gsr, temperature);
        }

        public ReadOnlyObservableCollection<Bvp> Bvp => _bvp;
        public ReadOnlyObservableCollection<Gsr> Gsr => _gsr;
        public ReadOnlyObservableCollection<Temperature> Temperature => _temperature;

        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }
}