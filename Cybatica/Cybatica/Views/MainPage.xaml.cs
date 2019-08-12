using Cybatica.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ReactiveTabbedPage<MainViewModel>
    {
        public MainPage()
        {
            ViewModel = new MainViewModel();
            InitializeComponent();
        }
    }
}