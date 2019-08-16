using Cybatica.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ReactiveContentPage<AboutViewModel>
    {
        public AboutPage()
        {
            ViewModel = new AboutViewModel();
            InitializeComponent();
        }
    }
}