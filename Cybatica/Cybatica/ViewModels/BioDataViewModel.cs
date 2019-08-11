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

        private readonly INavigation _navigation;

        private readonly ICybaticaHandler _cybaticaHandler;
        private readonly BioDataModel _bioData;

        public BioDataViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _cybaticaHandler = Locator.Current.GetService<ICybaticaHandler>();
            _bioData = _cybaticaHandler.BioDataModel;

            _ = this.WhenAnyValue(x => x._bioData.Bvp)
                .ToPropertyEx(this, x => x.Bvp);

            _ = this.WhenAnyValue(x => x._bioData.Ibi)
                .ToPropertyEx(this, x => x.Ibi);

            _ = this.WhenAnyValue(x => x._bioData.Hr)
                .ToPropertyEx(this, x => x.Hr);

            _ = this.WhenAnyValue(x => x._bioData.Gsr)
                .ToPropertyEx(this, x => x.Gsr);

            _ = this.WhenAnyValue(x => x._bioData.Temperature)
                .ToPropertyEx(this, x => x.Temperature);

            _ = this.WhenAnyValue(x => x._bioData.Acceleration)
                .ToPropertyEx(this, x => x.Acceleration);

            ChartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<BioDataChartViewModel>>() as Page;
                await _navigation.PushAsync(page);
            });
        }
    }
}
