using Android.App;
using Android.Content;
using Xamarin.Forms.Platform.Android;

namespace Cybatica.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", Icon = "@mipmap/icon_cybatica", MainLauncher = false,
        NoHistory = true)]
    public class SplashActivity : FormsAppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}