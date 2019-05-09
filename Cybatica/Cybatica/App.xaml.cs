using Cybatica.ViewModels;
using Cybatica.Views;
using ReactiveUI;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Cybatica
{
    public partial class App : Application
    {
        public App()
        {
            ExperimentalFeatures.Enable(ExperimentalFeatures.ShareFileRequest);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppPrivateInformations.SyncfusionLicenseKey);
            new AppBootstrapper();
            InitializeComponent();

            MainPage = Locator.Current.GetService<IViewFor<MainViewModel>>() as MainPage;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
