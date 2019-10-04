﻿using Cybatica.Empatica;
using Splat;
using Syncfusion.Licensing;
using Xamarin.Forms;

namespace Cybatica
{
    public partial class App : Application
    {
        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense(AppPrivateInformation.SyncfusionLicenseKey);
            var _ = new AppBootstrapper();
            var empaticaHandler = Locator.Current.GetService<IEmpaticaHandler>();
            empaticaHandler.Authenticate(AppPrivateInformation.EmpaticaApiKey);
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