using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Cybatica.Models;
using Cybatica.Services;
using DynamicData;
using ReactiveUI;
using Splat;

namespace Cybatica.ViewModels
{
    public class OcsChartViewModel : ReactiveObject, IDisposable
    {
        private readonly ReadOnlyObservableCollection<AnalysisData> _meanEda;
        private readonly ReadOnlyObservableCollection<AnalysisData> _ocs;
        private readonly ReadOnlyObservableCollection<AnalysisData> _sdNn;
        private readonly IDisposable _cleanUp;

        public OcsChartViewModel()
        {
            var handler = Locator.Current.GetService<ICybaticaHandler>();
            var session = handler.OcsSession;

            var ocs = session.Ocs.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ocs)
                .Subscribe();

            var sdNn = session.SdNn.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _sdNn)
                .Subscribe();

            var meanEda = session.MeanEda.Connect()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _meanEda)
                .Subscribe();

            _cleanUp = new CompositeDisposable(ocs, sdNn, meanEda);
        }

        public ReadOnlyObservableCollection<AnalysisData> Ocs => _ocs;
        public ReadOnlyObservableCollection<AnalysisData> SdNn => _sdNn;
        public ReadOnlyObservableCollection<AnalysisData> MeanEda => _meanEda;

        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }
}