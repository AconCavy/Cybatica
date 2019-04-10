using Cybatica.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using Splat;
using Cybatica.ViewModels;
using Cybatica.Empatica;

namespace Cybatica
{
    public partial class App : Application
    {
        private IEmpaticaAPI _empaticaAPI;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Empatica.EmpaticaPrivacyInformation.SyncfusionLicenseKey);

            InitializeComponent();
            new AppBootstrapper();

            _empaticaAPI = Locator.Current.GetService<IEmpaticaAPI>();

            MainPage = Locator.Current.GetService<IViewFor<MainViewModel>>() as MainPage;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            _empaticaAPI.PrepareForBackGround();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            _empaticaAPI.PrepareForResume();

        }
    }
}
