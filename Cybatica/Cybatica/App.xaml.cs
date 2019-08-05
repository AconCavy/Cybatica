using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cybatica
{
    public partial class App : Application
    {
        public App()
        {
            ExperimentalFeatures.Enable(ExperimentalFeatures.ShareFileRequest);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppPrivateInformations.SyncfusionLicenseKey);
            var appBootstrapper = new AppBootstrapper();
            InitializeComponent();

            MainPage = appBootstrapper.CreateMainPage();

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
