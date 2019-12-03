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
        public ReadOnlyObservableCollection<Bvp> Bvp => _bvp;
        public ReadOnlyObservableCollection<Gsr> Gsr => _gsr;
        public ReadOnlyObservableCollection<Temperature> Temperature => _temperature;

        private readonly IDisposable _cleanUp;
        private readonly ICybaticaHandler _cybaticaHandler;
        private readonly ReadOnlyObservableCollection<Bvp> _bvp;
        private readonly ReadOnlyObservableCollection<Gsr> _gsr;
        private readonly ReadOnlyObservableCollection<Temperature> _temperature;

        public BioDataChartViewModel(ICybaticaHandler cybaticaHandler = null)
        {
            _cybaticaHandler = cybaticaHandler ?? Locator.Current.GetService<ICybaticaHandler>();

            var bvp = _cybaticaHandler.BvpConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _bvp)
                .Subscribe();

            var gsr = _cybaticaHandler.GsrConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _gsr)
                .Subscribe();

            var temperature = _cybaticaHandler.TemperatureConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _temperature)
                .Subscribe();

            _cleanUp = new CompositeDisposable(bvp, gsr, temperature);
        }

        public void Dispose()
        {
            _cleanUp?.Dispose();
        }
    }
}