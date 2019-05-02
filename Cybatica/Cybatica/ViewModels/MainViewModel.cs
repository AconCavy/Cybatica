using System;
using System.Reactive.Disposables;
using Cybatica.Services;
using ReactiveUI;
using Splat;

namespace Cybatica.ViewModels
{
    public class MainViewModel : ReactiveObject, ISupportsActivation
    {
        public ViewModelActivator Activator { get; private set; }

        public MainViewModel()
        {
            Activator = new ViewModelActivator();

            this.WhenActivated(disposable =>
            {
                HandleActivation();

                Disposable.Create(() => HandleDeactivation())
                .DisposeWith(disposable);

            });
        }

        private void HandleActivation()
        {
            Console.WriteLine("MainViewModel: Activated");
            var _empaticaHandler = Locator.Current.GetService<IEmpaticaHandler>();
            _empaticaHandler.EmpaticaAPI.AuthenticateWithAPIKey(AppPrivateInformations.EmpaticaAPIKey);
        }

        private void HandleDeactivation()
        {
            Console.WriteLine("MainViewModel: Deactivated");
        }

        
    }
}
