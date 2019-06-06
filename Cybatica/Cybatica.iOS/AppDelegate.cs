﻿using E4linkBinding;
using Foundation;
using UIKit;

namespace Cybatica.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            new AppBootstrapper();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(232, 174, 183);

            LoadApplication(new App());
            return base.FinishedLaunching(app, options);

        }
        
        public override void DidEnterBackground(UIApplication uiApplication)
        {
            EmpaticaAPI.PrepareForBackground();
            
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            EmpaticaAPI.PrepareForResume();
            
        }
    }
}
