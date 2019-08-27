using E4linkBinding;
using Foundation;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Cybatica.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
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
            Forms.SetFlags("CollectionView_Experimental", "FastRenderers_Experimental");
            Forms.Init();
            FormsMaterial.Init();
            _ = new AppBootstrapper();

            SfChartRenderer.Init();

            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

        public override void DidEnterBackground(UIApplication uiApplication)
        {
            base.DidEnterBackground(uiApplication);
            EmpaticaAPI.PrepareForBackground();
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            EmpaticaAPI.PrepareForResume();
        }
    }
}