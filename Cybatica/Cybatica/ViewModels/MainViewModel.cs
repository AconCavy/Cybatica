using Cybatica.Services;
using ReactiveUI;
using System.Reactive.Disposables;

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
            var _handler = new CybaticaHandler();
            _handler.AuthenticateWithApiKey(AppPrivateInformations.EmpaticaAPIKey);
        }

        private void HandleDeactivation()
        {
        }

        
    }
}
