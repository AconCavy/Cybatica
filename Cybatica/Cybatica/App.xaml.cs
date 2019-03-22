﻿using Cybatica.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using Splat;
using Cybatica.ViewModels;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Cybatica
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var appBootstrapper = new AppBootstrapper();
            MainPage = new MainPage();
            //MainPage = new EmpaticaParameterPage(appBootstrapper.CreateEmpaticaParameterViewModel());

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