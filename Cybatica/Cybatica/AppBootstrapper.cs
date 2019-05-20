using System;
using ReactiveUI;
using Splat;
using Cybatica.Services;
using Cybatica.Views;
using Cybatica.ViewModels;
using System.Reflection;

namespace Cybatica
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterViewModels();
            RegisterViews();       
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }

        private void RegisterViews()
        {
            Locator.CurrentMutable.RegisterConstant(new MainPage(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(() =>
                new BioDataPage(), typeof(IViewFor<BioDataViewModel>));
            Locator.CurrentMutable.Register(() =>
                new CybersicknessPage(), typeof(IViewFor<CybersicknessViewModel>));
            Locator.CurrentMutable.Register(() =>
                new BioDataChartPage(), typeof(IViewFor<BioDataChartViewModel>));
        }

        private void RegisterViewModels()
        {
            Locator.CurrentMutable.RegisterConstant(new MainViewModel(), typeof(IReactiveObject), typeof(MainViewModel).FullName);
            Locator.CurrentMutable.Register(() => new BioDataViewModel(), typeof(IReactiveObject), typeof(BioDataViewModel).FullName);
            Locator.CurrentMutable.Register(() => new CybersicknessViewModel(), typeof(IReactiveObject), typeof(CybersicknessViewModel).FullName);
            Locator.CurrentMutable.Register(() => new BioDataChartViewModel(), typeof(IReactiveObject), typeof(BioDataChartViewModel).FullName);
        }

    }
}
