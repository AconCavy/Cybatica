using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System.Reactive.Disposables;

namespace Cybatica.Views
{
    public partial class EmpaticaParameterPage : ReactiveContentPage<EmpaticaParameterViewModel>
	{
        public EmpaticaParameterPage()
        {
            InitializeComponent();
        }

        public EmpaticaParameterPage(EmpaticaParameterViewModel viewModel)
        {
            this.ViewModel = viewModel;

            this.InitializeComponent();

            this.WhenActivated(disposable =>
            {

                this.OneWayBind(this.ViewModel,
                    vm => vm.BVP,
                    v => v.BVP.Text,
                    x => x.ToString())
                    .DisposeWith(disposable);

                this.OneWayBind(this.ViewModel,
                    vm => vm.HR,
                    v => v.HR.Text,
                    x => x.ToString())
                    .DisposeWith(disposable);

                this.OneWayBind(this.ViewModel,
                   vm => vm.IBI,
                   v => v.IBI.Text,
                   x => x.ToString())
                   .DisposeWith(disposable);

                this.OneWayBind(this.ViewModel,
                   vm => vm.GSR,
                   v => v.GSR.Text,
                   x => x.ToString())
                   .DisposeWith(disposable);

                this.OneWayBind(this.ViewModel,
                    vm => vm.Temperature,
                    v => v.Temperature.Text,
                    x => x.ToString())
                    .DisposeWith(disposable);

                this.BindCommand(this.ViewModel,
                    vm => vm.DiscoverDeviceCommand,
                    v => v.DiscoverDeviceCommand)
                    .DisposeWith(disposable);

                this.BindCommand(this.ViewModel,
                    vm => vm.DisconnectCommand,
                    v => v.Disconnect)
                    .DisposeWith(disposable);
            });
        }
        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //ViewModel.Activator.Activate();
        }



    }
}