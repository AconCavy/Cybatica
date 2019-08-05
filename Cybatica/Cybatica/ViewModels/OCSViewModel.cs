using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System.Reactive;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class OCSViewModel : ReactiveObject, IRootNavigation
    {
        [Reactive] public float Ocs { get; private set; }
        [Reactive] public float NnMean { get; private set; }
        [Reactive] public float SdNn { get; private set; }
        [Reactive] public float MeanEda { get; private set; }
        [Reactive] public float PeakEda { get; private set; }

        public ReactiveCommand<Unit, Unit> ChartCommand { get; private set; }

        public INavigation Navigation { get; private set; }

        public OCSViewModel(INavigation navigation)
        {
            Navigation = navigation;

            ChartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<OCSChartViewModel>>() as Page;
                await Navigation.PushAsync(page);
            });
        }

    }

}
