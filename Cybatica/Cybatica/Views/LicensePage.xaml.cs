using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LicensePage : ReactiveContentPage<LicenseViewModel>
    {
        public LicensePage()
        {
            ViewModel = new LicenseViewModel();
            InitializeComponent();
        }
    }
}