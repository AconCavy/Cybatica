using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Cybatica.Empatica;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace Cybatica.Droid
{
    [Activity(Label = "Cybatica", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        private const int RequestEnableBt = 1;
        private const int RequestPermissionAccessCoarseLocation = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);

            _ = new AppBootstrapper();
            var empaticaHandler = Locator.Current.GetService<IEmpaticaHandler>();
            empaticaHandler.Authenticate(AppPrivateInformations.EmpaticaApiKey);

            if (ApplicationContext.CheckCallingOrSelfPermission(
                    Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
                ActivityCompat.RequestPermissions(this, new[] {Manifest.Permission.AccessCoarseLocation},
                    RequestPermissionAccessCoarseLocation);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == RequestEnableBt && resultCode == Result.Canceled) return;
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}