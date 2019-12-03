using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Cybatica.Models;
using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class OcsViewModel : ReactiveObject
    {
        private readonly ICybaticaHandler _cybaticaHandler;

        public OcsViewModel(ICybaticaHandler cybaticaHandler = null)
        {
            _cybaticaHandler = cybaticaHandler ?? Locator.Current.GetService<ICybaticaHandler>();

            this.WhenAnyValue(x => x._cybaticaHandler.Ocs)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Ocs);

            this.WhenAnyValue(x => x._cybaticaHandler.NnMean)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.NnMean);

            this.WhenAnyValue(x => x._cybaticaHandler.SdNn)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.SdNn);

            this.WhenAnyValue(x => x._cybaticaHandler.MeanEda)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.MeanEda);

            this.WhenAnyValue(x => x._cybaticaHandler.PeakEda)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.PeakEda);

            ChartCommand = ReactiveCommand.CreateFromTask(async () => { await Shell.Current.GoToAsync("ocsChart"); });
        }

        [Reactive] public AnalysisData Ocs { [ObservableAsProperty] get; private set; }
        [Reactive] public AnalysisData NnMean { [ObservableAsProperty] get; private set; }
        [Reactive] public AnalysisData SdNn { [ObservableAsProperty] get; private set; }
        [Reactive] public AnalysisData MeanEda { [ObservableAsProperty] get; private set; }
        [Reactive] public AnalysisData PeakEda { [ObservableAsProperty] get; private set; }

        public ICommand ChartCommand { get; }
    }
}