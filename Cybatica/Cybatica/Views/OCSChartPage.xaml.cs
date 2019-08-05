using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
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
        }
    }
}