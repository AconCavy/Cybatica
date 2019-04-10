using System;
using ReactiveUI;
using Splat;
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
            Locator.CurrentMutable.RegisterLazySingleton(() =>
                new EmpaticaParameterPage(), typeof(IViewFor<EmpaticaParameterViewModel>));
            Locator.CurrentMutable.RegisterLazySingleton(() =>
                new CybersicknessPage(), typeof(IViewFor<CybersicknessViewModel>));
            Locator.CurrentMutable.Register(() =>
                new EmpaticaChartPage(), typeof(IViewFor<EmpaticaChartViewModel>));
        }

        private void RegisterViewModels()
        {
            Locator.CurrentMutable.RegisterConstant(new MainViewModel(), typeof(IReactiveObject), typeof(MainViewModel).FullName);
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaParameterViewModel(), typeof(IReactiveObject), typeof(EmpaticaParameterViewModel).FullName);
            Locator.CurrentMutable.RegisterLazySingleton(() => new CybersicknessViewModel(), typeof(IReactiveObject), typeof(CybersicknessViewModel).FullName);
            Locator.CurrentMutable.Register(() => new EmpaticaChartViewModel(), typeof(IReactiveObject), typeof(EmpaticaChartViewModel).FullName);
        }

        

    }
}
