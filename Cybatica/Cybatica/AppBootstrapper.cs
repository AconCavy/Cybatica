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

        private static void RegisterDependencies()
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
                () => new AboutPage(), typeof(IViewFor<AboutViewModel>));
            Locator.CurrentMutable.Register(
                () => new OcsPage(), typeof(IViewFor<OcsViewModel>));
            Locator.CurrentMutable.Register(
                () => new OcsChartPage(), typeof(IViewFor<OcsChartViewModel>));
            Locator.CurrentMutable.Register(
                () => new BioDataPage(), typeof(IViewFor<BioDataViewModel>));
            Locator.CurrentMutable.Register(
                () => new BioDataChartPage(), typeof(IViewFor<BioDataChartViewModel>));
        }
    }
}