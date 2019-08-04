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
            ViewModel = Locator.Current.GetService<IReactiveObject>(typeof(HomeViewModel).FullName)
                as HomeViewModel;
            ViewModel.Navigation = Navigation;

            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                disposable(this.OneWayBind(ViewModel,
                    vm => vm.ElapsedTime,
                    v => v.ElapsedTime.Text,
                    x => x.ToString()));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.Stop,
                    v => v.Stop));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.CaptureBase,
                    v => v.CaptureBase));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.CaptureData,
                    v => v.CaptureData));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.Connect,
                    v => v.Connect));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.Disconnect,
                    v => v.Disconnect));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.Licenses,
                    v => v.Licenses));

            });
        }
    }
}
