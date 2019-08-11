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
    public class BioDataChartViewModel : ReactiveObject, IDisposable
    {
        public ReadOnlyObservableCollection<Bvp> Bvp => _bvp;
        public ReadOnlyObservableCollection<Gsr> Gsr => _gsr;
        public ReadOnlyObservableCollection<Temperature> Temperature => _temperature;

        private readonly ReadOnlyObservableCollection<Bvp> _bvp;
        private readonly ReadOnlyObservableCollection<Gsr> _gsr;
        private readonly ReadOnlyObservableCollection<Temperature> _temperature;

        private readonly ICybaticaHandler _handler;
        private readonly EmpaticaSession _session;

        private readonly IDisposable _cleanUp;

        public BioDataChartViewModel()
        {
            _handler = Locator.Current.GetService<ICybaticaHandler>();
            _session = _handler.EmpaticaSession;

            var bvp = _session.Bvp.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _bvp)
                .Subscribe();

            var gsr = _session.Gsr.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _gsr)
                .Subscribe();

            var temperature = _session.Temperature.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _temperature)
                .Subscribe();

            _cleanUp = new CompositeDisposable(bvp, gsr, temperature);
        }

        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }

}
