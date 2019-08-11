using Cybatica.Models;
using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Reactive;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class OCSViewModel : ReactiveObject, IDisposable
    {
        
        [Reactive] public float Ocs { [ObservableAsProperty] get; private set; }
        [Reactive] public float NnMean { [ObservableAsProperty] get; private set; }
        [Reactive] public float SdNn { [ObservableAsProperty] get; private set; }
        [Reactive] public float MeanEda { [ObservableAsProperty] get; private set; }
        [Reactive] public float PeakEda { [ObservableAsProperty] get; private set; }
        

        public ReactiveCommand<Unit, Unit> ChartCommand { get; private set; }

        private readonly INavigation _navigation;
        private readonly ICybaticaHandler _cybaticaHandler;

        private readonly OCSModel _ocsModel;

        public OCSViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _cybaticaHandler = Locator.Current.GetService<ICybaticaHandler>();

            _ocsModel = _cybaticaHandler.OcsModel;

            this.WhenAnyValue(x => x._ocsModel.Ocs)
                .ToPropertyEx(this, x => x.Ocs);

            this.WhenAnyValue(x => x._ocsModel.NnMean)
                .ToPropertyEx(this, x => x.NnMean);

            this.WhenAnyValue(x => x._ocsModel.SdNn)
                .ToPropertyEx(this, x => x.SdNn);

            this.WhenAnyValue(x => x._ocsModel.MeanEda)
                .ToPropertyEx(this, x => x.MeanEda);

            this.WhenAnyValue(x => x._ocsModel.PeakEda)
                .ToPropertyEx(this, x => x.PeakEda);

            ChartCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<OCSChartViewModel>>() as Page;
                await _navigation.PushAsync(page);
            });
        }

        public void Dispose()
        {
        }
    }

}
