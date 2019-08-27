using Cybatica.ViewModels;
using ReactiveUI;
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

            this.WhenActivated(disposable =>
            {
                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Licenses,
                    v => v.Licenses.ItemsSource));
            });
        }
    }
}