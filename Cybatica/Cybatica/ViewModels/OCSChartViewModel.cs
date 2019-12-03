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
    public class OcsChartViewModel : ReactiveObject, IDisposable
    {
        public ReadOnlyObservableCollection<AnalysisData> Ocs => _ocs;
        public ReadOnlyObservableCollection<AnalysisData> SdNn => _sdNn;
        public ReadOnlyObservableCollection<AnalysisData> MeanEda => _meanEda;

        private readonly IDisposable _cleanUp;
        private readonly ICybaticaHandler _cybaticaHandler;
        private readonly ReadOnlyObservableCollection<AnalysisData> _ocs;
        private readonly ReadOnlyObservableCollection<AnalysisData> _sdNn;
        private readonly ReadOnlyObservableCollection<AnalysisData> _meanEda;

        public OcsChartViewModel(ICybaticaHandler cybaticaHandler = null)
        {
            _cybaticaHandler = cybaticaHandler ?? Locator.Current.GetService<ICybaticaHandler>();

            var ocs = _cybaticaHandler.OcsConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ocs)
                .Subscribe();

            var sdNn = _cybaticaHandler.SdNnConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _sdNn)
                .Subscribe();

            var meanEda = _cybaticaHandler.MeanEdaConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _meanEda)
                .Subscribe();

            _cleanUp = new CompositeDisposable(ocs, sdNn, meanEda);
        }

        public void Dispose()
        {
            _cleanUp?.Dispose();
        }
    }
}