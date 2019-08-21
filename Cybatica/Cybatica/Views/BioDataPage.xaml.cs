using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BioDataPage : ReactiveContentPage<BioDataViewModel>
    {
        public BioDataPage()
        {
            ViewModel = new BioDataViewModel(Navigation);
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Bvp,
                    v => v.Bvp.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Hr,
                    v => v.Hr.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Ibi,
                    v => v.Ibi.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Gsr,
                    v => v.Gsr.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Temperature,
                    v => v.Temperature.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Acceleration,
                    v => v.Acceleration.Text,
                    x => $"x:{x.XValue:F2}, y:{x.YValue:F2}, z:{x.ZValue:F2}"));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.ChartCommand,
                    v => v.Chart));
            });
        }
    }
}