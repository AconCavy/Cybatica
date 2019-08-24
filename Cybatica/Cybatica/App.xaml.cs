using Cybatica.Empatica;
using Splat;
using Syncfusion.Licensing;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cybatica
{
    public partial class App : Application
    {
        public App()
        {
            ExperimentalFeatures.Enable(ExperimentalFeatures.ShareFileRequest);
            SyncfusionLicenseProvider.RegisterLicense(AppPrivateInformations.SyncfusionLicenseKey);
            var _ = new AppBootstrapper();
            var empaticaHandler = Locator.Current.GetService<IEmpaticaHandler>();
            empaticaHandler.Authenticate(AppPrivateInformations.EmpaticaApiKey);
            InitializeComponent();

            MainPage = new AppShell();
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