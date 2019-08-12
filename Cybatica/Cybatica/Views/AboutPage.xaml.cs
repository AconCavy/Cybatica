using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
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