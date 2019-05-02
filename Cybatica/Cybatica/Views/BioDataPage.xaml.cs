using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System.Reactive.Disposables;

namespace Cybatica.Views
{
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
                    vm => vm.BVP,
                    v => v.BVP.Text,
                    x => x.ToString("F2"))
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.HR,
                    v => v.HR.Text,
                    x => x.ToString("F2"))
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.IBI,
                    v => v.IBI.Text,
                    x => x.ToString("F2"))
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.GSR,
                    v => v.GSR.Text,
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