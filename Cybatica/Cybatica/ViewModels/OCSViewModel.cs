using Cybatica.Models;
using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System.Reactive;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class OCSViewModel : ReactiveObject
    {
        [Reactive] public float Ocs { [ObservableAsProperty] get; private set; }
        [Reactive] public float NnMean { [ObservableAsProperty] get; private set; }
        [Reactive] public float SdNn { [ObservableAsProperty] get; private set; }
        [Reactive] public float MeanEda { [ObservableAsProperty] get; private set; }
        [Reactive] public float PeakEda { [ObservableAsProperty] get; private set; }

        public ReactiveCommand<Unit, Unit> ChartCommand { get; private set; }

        public INavigation Navigation { get; private set; }

        private readonly ICybaticaHandler _cybaticaHandler;
        private readonly OCSModel _ocs;

        public OCSViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _cybaticaHandler = Locator.Current.GetService<ICybaticaHandler>();
            _ocs = new OCSModel(_cybaticaHandler.EmpaticaSession);

            this.WhenAnyValue(x => x._ocs.Ocs)
                .ToPropertyEx(this, x => x.Ocs);

            this.WhenAnyValue(x => x._ocs.NnMean)
                .ToPropertyEx(this, x => x.NnMean);

            this.WhenAnyValue(x => x._ocs.SdNn)
                .ToPropertyEx(this, x => x.SdNn);

            this.WhenAnyValue(x => x._ocs.MeanEda)
                .ToPropertyEx(this, x => x.MeanEda);

            this.WhenAnyValue(x => x._ocs.PeakEda)
                .ToPropertyEx(this, x => x.PeakEda);

            ChartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<OCSChartViewModel>>() as Page;
                await Navigation.PushAsync(page);
            });
        }

    }

}
