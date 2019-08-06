using Cybatica.Models;
using Cybatica.Services;
using Cybatica.ViewModels;
using Cybatica.Views;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace Cybatica
{
    public class AppBootstrapper : ReactiveObject
    {
        public AppBootstrapper()
        {
            RegisterDependencies();
        }

        public Page CreateMainPage()
        {
            return Locator.Current.GetService<IViewFor<MainViewModel>>() as MainPage;
        }

        private void RegisterDependencies()
        {
            // Services
            Locator.CurrentMutable.RegisterLazySingleton(
                () => new CybaticaHandler(), typeof(ICybaticaHandler));

            // Views
            Locator.CurrentMutable.RegisterConstant(
                new MainPage(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(
                () => new HomePage(), typeof(IViewFor<HomeViewModel>));
            Locator.CurrentMutable.Register(
                () => new LicensePage(), typeof(IViewFor<LicenseViewModel>));
            Locator.CurrentMutable.Register(
                () => new OCSPage(), typeof(IViewFor<OCSViewModel>));
            Locator.CurrentMutable.Register(
                () => new OCSChartPage(), typeof(IViewFor<OCSChartViewModel>));
            Locator.CurrentMutable.Register(
                () => new BioDataPage(), typeof(IViewFor<BioDataViewModel>));
            Locator.CurrentMutable.Register(
                () => new BioDataChartPage(), typeof(IViewFor<BioDataChartViewModel>));
        }

    }
}
