using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;

namespace JZipSearch.DroidGawa
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            var webView = FindViewById<Android.Webkit.WebView>(Resource.Id.webView);
            webView.SetWebViewClient(new Android.Webkit.WebViewClient());
            webView.Settings.JavaScriptEnabled = true;
            webView.LoadUrl("https://decode19sap.azurewebsites.net/index.html");
        }
        /*
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            var webView = FindViewById<Android.Webkit.WebView>(Resource.Id.webView);
            menu.GetItem(0).SetEnabled(webView.CanGoBack());
            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            var webView = FindViewById<Android.Webkit.WebView>(Resource.Id.webView);
            menu.GetItem(0).SetEnabled(webView.CanGoBack());
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                var webView = FindViewById<Android.Webkit.WebView>(Resource.Id.webView);
                if (webView.CanGoBack())
                    webView.GoBack();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
        */
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}