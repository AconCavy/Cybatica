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
            RegisterEmpatica();
            RegisterViewModels();
            RegisterViews();       
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }

        private void RegisterViews()
        {
            Locator.CurrentMutable.RegisterConstant(new MainPage(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(() =>
                new Views.BioDataPage(), typeof(IViewFor<ViewModels.BioDataViewModel>));
            Locator.CurrentMutable.Register(() =>
                new CybersicknessPage(), typeof(IViewFor<CybersicknessViewModel>));
            Locator.CurrentMutable.Register(() =>
                new EmpaticaChartPage(), typeof(IViewFor<EmpaticaChartViewModel>));
        }

        private void RegisterViewModels()
        {
            Locator.CurrentMutable.RegisterConstant(new MainViewModel(), typeof(IReactiveObject), typeof(MainViewModel).FullName);
            Locator.CurrentMutable.Register(() => new ViewModels.BioDataViewModel(), typeof(IReactiveObject), typeof(ViewModels.BioDataViewModel).FullName);
            Locator.CurrentMutable.Register(() => new CybersicknessViewModel(), typeof(IReactiveObject), typeof(CybersicknessViewModel).FullName);
            Locator.CurrentMutable.Register(() => new EmpaticaChartViewModel(), typeof(IReactiveObject), typeof(EmpaticaChartViewModel).FullName);
        }

        private void RegisterEmpatica()
        {
            Locator.CurrentMutable.RegisterConstant(new EmpaticaHandler(), typeof(IEmpaticaHandler));
        }

    }
}
