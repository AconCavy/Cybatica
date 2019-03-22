using System;
using ReactiveUI;
using Splat;
using Cybatica.Views;
using Cybatica.ViewModels;

namespace Cybatica
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterViews();
            RegisterViewModels();
        }

        public EmpaticaParameterViewModel CreateEmpaticaParameterViewModel()
        {
            return new EmpaticaParameterViewModel();
        }

        private void RegisterViews()
        {
            // Locator.CurrentMutable.Register(() => new EmpaticaParameterPage(), typeof(IViewFor<EmpaticaParameterViewModel>));
        }

        private void RegisterViewModels()
        {
            //  Locator.CurrentMutable.Register(() => new EmpaticaParameterViewModel(), typeof(IRoutableViewModel), typeof(EmpaticaParameterViewModel).FullName);
        }

        

    }
}
