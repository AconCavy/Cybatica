using Cybatica.ViewModels;
using Cybatica.Views;
using ReactiveUI;
using Splat;
using System.Reflection;

namespace Cybatica
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterViewModels();
            RegisterViews();
            //Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }

        private void RegisterViews()
        {
            Locator.CurrentMutable.RegisterConstant(
                new MainPage(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(
                () => new HomePage(), typeof(IViewFor<HomeViewModel>));
            Locator.CurrentMutable.Register(
                () => new LicensePage(), typeof(IViewFor<LicenseViewModel>));
            Locator.CurrentMutable.Register(
                () => new BioDataPage(), typeof(IViewFor<BioDataViewModel>));
            Locator.CurrentMutable.Register(
                () => new OCSPage(), typeof(IViewFor<OCSViewModel>));
            Locator.CurrentMutable.Register(
                () => new BioDataChartPage(), typeof(IViewFor<BioDataChartViewModel>));
        }

        private void RegisterViewModels()
        {
            Locator.CurrentMutable.RegisterConstant(
                new MainViewModel(), typeof(IReactiveObject), typeof(MainViewModel).FullName);
            Locator.CurrentMutable.Register(
                () => new HomeViewModel(), typeof(IReactiveObject), typeof(HomeViewModel).FullName);
            Locator.CurrentMutable.Register(
                () => new LicenseViewModel(), typeof(IReactiveObject), typeof(LicenseViewModel).FullName);
            Locator.CurrentMutable.Register(
                () => new BioDataViewModel(), typeof(IReactiveObject), typeof(BioDataViewModel).FullName);
            Locator.CurrentMutable.Register(
                () => new OCSViewModel(), typeof(IReactiveObject), typeof(OCSViewModel).FullName);
            Locator.CurrentMutable.Register(
                () => new BioDataChartViewModel(), typeof(IReactiveObject), typeof(BioDataChartViewModel).FullName);
        }

    }
}
