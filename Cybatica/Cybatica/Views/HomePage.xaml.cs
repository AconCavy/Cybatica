using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ReactiveContentPage<HomeViewModel>
    {
        public HomePage()
        {
            ViewModel = new HomeViewModel(Navigation);

            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                disposable(this.OneWayBind(ViewModel,
                    vm => vm.ElapsedTime,
                    v => v.ElapsedTime.Text,
                    x => x.ToString(@"d'.'hh':'mm':'ss'.'ff")));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.StartBaseSessionCommand,
                    v => v.StartBaseSession));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.StartDataSessionCommand,
                    v => v.StartDataSession));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.ConnectCommand,
                    v => v.Connect));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.DisconnectCommand,
                    v => v.Disconnect));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.StopSessionCommand,
                    v => v.Stop));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.AboutCommand,
                    v => v.About));
            });
        }
    }
}