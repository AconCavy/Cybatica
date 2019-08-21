using System;
using System.Reactive;
using System.Reactive.Linq;
using Cybatica.Empatica;
using Cybatica.Models;
using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class BioDataViewModel : ReactiveObject
    {
        private readonly BioDataModel _bioData;

        public BioDataViewModel(INavigation navigation)
        {
            var cybaticaHandler = Locator.Current.GetService<ICybaticaHandler>();
            _bioData = cybaticaHandler.BioDataModel;

            _ = this.WhenAnyValue(x => x._bioData.Bvp)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Bvp);

            _ = this.WhenAnyValue(x => x._bioData.Ibi)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Ibi);

            _ = this.WhenAnyValue(x => x._bioData.Hr)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Hr);

            _ = this.WhenAnyValue(x => x._bioData.Gsr)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Gsr);

            _ = this.WhenAnyValue(x => x._bioData.Temperature)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Temperature);

            _ = this.WhenAnyValue(x => x._bioData.Acceleration)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Acceleration);

            ChartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<BioDataChartViewModel>>() as Page;
                await navigation.PushAsync(page);
            });
        }

        [Reactive] public Bvp Bvp { [ObservableAsProperty] get; private set; }
        [Reactive] public Ibi Ibi { [ObservableAsProperty] get; private set; }
        [Reactive] public Hr Hr { [ObservableAsProperty] get; private set; }
        [Reactive] public Gsr Gsr { [ObservableAsProperty] get; private set; }
        [Reactive] public Temperature Temperature { [ObservableAsProperty] get; private set; }
        [Reactive] public Acceleration Acceleration { [ObservableAsProperty] get; private set; }

        public ReactiveCommand<Unit, Unit> ChartCommand { get; }
    }
}