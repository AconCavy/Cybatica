using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OcsPage : ReactiveContentPage<OcsViewModel>
    {
        public OcsPage()
        {
            ViewModel = new OcsViewModel();
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Ocs,
                    v => v.Ocs.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.NnMean,
                    v => v.NnMean.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.SdNn,
                    v => v.SdNn.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.MeanEda,
                    v => v.MeanEda.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.PeakEda,
                    v => v.PeakEda.Text,
                    x => x.Value.ToString("F2")));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.ChartCommand,
                    v => v.Chart));
            });
        }
    }
}