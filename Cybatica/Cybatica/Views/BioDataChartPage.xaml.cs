using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BioDataChartPage : ReactiveContentPage<BioDataChartViewModel>
    {
        public BioDataChartPage()
        {
            ViewModel = new BioDataChartViewModel();
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Bvp,
                    v => v.Bvp.ItemsSource));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Gsr,
                    v => v.Eda.ItemsSource));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Temperature,
                    v => v.Temperature.ItemsSource));

            });

        }

    }

}