using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BioDataPage : ReactiveContentPage<BioDataViewModel>
	{
        public BioDataPage()
        {
            ViewModel = Locator.Current.GetService<IReactiveObject>(typeof(BioDataViewModel).FullName)
                as BioDataViewModel;
            ViewModel.Navigation = Navigation;

            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.Bvp,
                    v => v.Bvp.Text,
                    x => x.ToString("F2"))
                    .DisposeWith(disposable);

                /*
                this.OneWayBind(ViewModel,
                    vm => vm.Hr,
                    v => v.Hr.Text,
                    x => x.ToString("F2"))
                    .DisposeWith(disposable);
                    */
                this.OneWayBind(ViewModel,
                    vm => vm.Ibi,
                    v => v.Ibi.Text,
                    x => x.ToString("F2"))
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Gsr,
                    v => v.Gsr.Text,
                    x => x.ToString("F2"))
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Temperature,
                    v => v.Temperature.Text,
                    x => x.ToString("F2"))
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    vm => vm.NavigateToChartPage,
                    v => v.NavigateChartCommand)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    vm => vm.ManageDevice,
                    v => v.ManageDeviceCommand)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    vm => vm.DisconnectDevice,
                    v => v.DisconnectDeviceCommand)
                    .DisposeWith(disposable);

            });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Activator.Activate();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.Activator.Deactivate();

        }



    }
}