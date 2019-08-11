using Cybatica.Models;
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
    public class OCSChartViewModel : ReactiveObject, IDisposable
    {
        public ReadOnlyObservableCollection<AnalysisData> Ocs => _ocs;
        public ReadOnlyObservableCollection<AnalysisData> SdNn => _sdNn;
        public ReadOnlyObservableCollection<AnalysisData> MeanEda => _meanEda;

        private readonly ReadOnlyObservableCollection<AnalysisData> _ocs;
        private readonly ReadOnlyObservableCollection<AnalysisData> _sdNn;
        private readonly ReadOnlyObservableCollection<AnalysisData> _meanEda;

        private readonly ICybaticaHandler _handler;
        private readonly OCSSession _session;

        private readonly IDisposable _cleanUp;

        public OCSChartViewModel()
        {
            _handler = Locator.Current.GetService<ICybaticaHandler>();
            _session = _handler.OCSSession;

            var ocs = _session.Ocs.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ocs)
                .Subscribe();

            var sdNn = _session.SdNn.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _sdNn)
                .Subscribe();

            var meanEda = _session.MeanEda.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _meanEda)
                .Subscribe();

            _cleanUp = new CompositeDisposable(ocs, sdNn, meanEda);

        }

        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }
}
