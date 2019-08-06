using Cybatica.Empatica;
using Cybatica.Models;
using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System.Reactive;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class BioDataViewModel : ReactiveObject
    {
        [Reactive] public Bvp Bvp { [ObservableAsProperty] get; private set; }
        [Reactive] public Ibi Ibi { [ObservableAsProperty] get; private set; }
        [Reactive] public Hr Hr { [ObservableAsProperty] get; private set; }
        [Reactive] public Gsr Gsr { [ObservableAsProperty]  get; private set; }
        [Reactive] public Temperature Temperature { [ObservableAsProperty] get; private set; }
        [Reactive] public Acceleration Acceleration { [ObservableAsProperty] get; private set; }

        public ReactiveCommand<Unit, Unit> ChartCommand { get; private set; }

        public INavigation Navigation { get; private set; }

        private readonly ICybaticaHandler _cybaticaHandler;
        private readonly BioDataModel _bioData;

        public BioDataViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _cybaticaHandler = Locator.Current.GetService<ICybaticaHandler>();
            _bioData = new BioDataModel(_cybaticaHandler.EmpaticaSession);

            this.WhenAnyValue(x => x._bioData.Bvp)
                .ToPropertyEx(this, x => x.Bvp);

            this.WhenAnyValue(x => x._bioData.Ibi)
                .ToPropertyEx(this, x => x.Ibi);

            this.WhenAnyValue(x => x._bioData.Hr)
                .ToPropertyEx(this, x => x.Hr);

            this.WhenAnyValue(x => x._bioData.Gsr)
                .ToPropertyEx(this, x => x.Gsr);

            this.WhenAnyValue(x => x._bioData.Temperature)
                .ToPropertyEx(this, x => x.Temperature);

            this.WhenAnyValue(x => x._bioData.Acceleration)
                .ToPropertyEx(this, x => x.Acceleration);

            ChartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<BioDataChartViewModel>>() as Page;
                await Navigation.PushAsync(page);
            });

        }

    }

}
