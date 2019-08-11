using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OCSChartPage : ReactiveContentPage<OCSChartViewModel>
    {
        public OCSChartPage()
        {
            ViewModel = new OCSChartViewModel();
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Ocs,
                    v => v.Ocs.ItemsSource));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.SdNn,
                    v => v.SdNn.ItemsSource));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.MeanEda,
                    v => v.MeanEda.ItemsSource));

            });
        }
    }
}