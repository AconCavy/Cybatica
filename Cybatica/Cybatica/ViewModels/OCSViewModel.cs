using System;
using System.Reactive;
using System.Reactive.Linq;
using Cybatica.Models;
using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class OcsViewModel : ReactiveObject, IDisposable
    {
        private readonly OcsModel _ocsModel;

        public OcsViewModel()
        {
            var cybaticaHandler = Locator.Current.GetService<ICybaticaHandler>();
            _ocsModel = cybaticaHandler.OcsModel;

            this.WhenAnyValue(x => x._ocsModel.Ocs)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Ocs);

            this.WhenAnyValue(x => x._ocsModel.NnMean)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.NnMean);

            this.WhenAnyValue(x => x._ocsModel.SdNn)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.SdNn);

            this.WhenAnyValue(x => x._ocsModel.MeanEda)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.MeanEda);

            this.WhenAnyValue(x => x._ocsModel.PeakEda)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.PeakEda);

            ChartCommand = ReactiveCommand.CreateFromTask(async () => { await Shell.Current.GoToAsync("ocsChart"); });
        }

        [Reactive] public float Ocs { [ObservableAsProperty] get; private set; }
        [Reactive] public float NnMean { [ObservableAsProperty] get; private set; }
        [Reactive] public float SdNn { [ObservableAsProperty] get; private set; }
        [Reactive] public float MeanEda { [ObservableAsProperty] get; private set; }
        [Reactive] public float PeakEda { [ObservableAsProperty] get; private set; }


        public ReactiveCommand<Unit, Unit> ChartCommand { get; }

        public void Dispose()
        {
        }
    }
}