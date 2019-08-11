using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Cybatica.Empatica;
using Splat;

namespace Cybatica.Droid
{
    [Activity(Label = "Cybatica", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly int _requestEnableBt = 1;
        private readonly int _requestPermissionAccessCoarseLocation = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            _ = new AppBootstrapper();
            var empaticaHandler = Locator.Current.GetService<IEmpaticaHandler>();
            empaticaHandler.Authenticate(AppPrivateInformations.EmpaticaAPIKey);

            if (ApplicationContext.CheckCallingOrSelfPermission(
                Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessCoarseLocation },
                    _requestPermissionAccessCoarseLocation);
            }

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == _requestEnableBt && resultCode == Result.Canceled)
            {
                return;
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
