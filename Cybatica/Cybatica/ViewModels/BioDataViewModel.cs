using Cybatica.Empatica;
using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System.Reactive;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class BioDataViewModel : ReactiveObject, IRootNavigation
    {
        [Reactive] public Bvp Bvp { get; private set; }
        [Reactive] public Ibi Ibi { get; private set; }
        [Reactive] public Hr Hr { get; private set; }
        [Reactive] public Gsr Gsr { get; private set; }
        [Reactive] public Temperature Temperature { get; private set; }
        [Reactive] public Acceleration Acceleration { get; private set; }

        public ReactiveCommand<Unit, Unit> ChartCommand { get; private set; }

        public INavigation Navigation { get; private set; }

        public BioDataViewModel(INavigation navigation)
        {
            Navigation = navigation;

            ChartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<BioDataChartViewModel>>() as Page;
                await Navigation.PushAsync(page);
            });

        }

    }

}
