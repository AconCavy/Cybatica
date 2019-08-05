using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
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
                    x => x.ToString()));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.StopCommand,
                    v => v.Stop));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.CaptureBaseCommand,
                    v => v.CaptureBase));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.CaptureDataCommand,
                    v => v.CaptureData));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.ConnectCommand,
                    v => v.Connect));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.DisconnectCommand,
                    v => v.Disconnect));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.LicensesCommand,
                    v => v.Licenses));

            });
        }
    }
}
