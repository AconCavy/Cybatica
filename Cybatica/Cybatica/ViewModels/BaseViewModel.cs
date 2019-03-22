using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Cybatica.ViewModels
{
    public class BaseViewModel : ReactiveObject, IScreen
    {
        [Reactive]
        public bool IsBusy { get; set; }

        [Reactive]
        public string Title { get; set; }

        public RoutingState Router { get; }

        public BaseViewModel()
        {
            Router = new RoutingState();
        }
    }
}
