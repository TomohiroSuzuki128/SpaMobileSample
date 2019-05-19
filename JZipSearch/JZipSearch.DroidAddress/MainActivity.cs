using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using JZipCodeSearchClient;

namespace JZipSearch.DroidAddress
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var fromAddressSpinner = FindViewById<Spinner>(Resource.Id.fromAddressSpinner);
            var fromAddressTextEdit = FindViewById<EditText>(Resource.Id.fromAddressTextEdit);
            var fromAddressButton = FindViewById<Button>(Resource.Id.fromAddressButton);
            var zipCodeTextEdit = FindViewById<EditText>(Resource.Id.zipCodeTextEdit);

            var prefAddapter = new SpinnerAdapter(this, JZipCodeSearchClient.Prefectures.All()?.ToList());
            fromAddressSpinner.Adapter = prefAddapter;

            fromAddressButton.Click += async (sender, e) =>
            {

                var pref = prefAddapter[fromAddressSpinner.SelectedItemPosition].Code;
                var address = fromAddressTextEdit.Text;
                if(string.IsNullOrWhiteSpace(pref) || string.IsNullOrWhiteSpace(address))
                {
                    ShowToast("都道府県と市区町村の両方を入力してください.");
                    return;
                }
                var addressList = await JZipSearch.Core.JZipSearchClient.AddressToZip(pref, address);
                if (addressList?.Any() == false)
                {
                    ShowNoItemToast();
                    return;
                }
                if (addressList?.Count() > 1)
                {
                    ShowToast("郵便番号が特定できませんでした.");
                    return;
                }
                var zip = addressList.FirstOrDefault();
                zipCodeTextEdit.Text = zip.ZipCode;
            };
        }

        void ShowToast(string message)
        {
            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();
        }

        void ShowNoItemToast() => ShowToast("該当する情報が見つかりません.");

        class SpinnerAdapter : BaseAdapter<Prefecture>
        {
            List<Prefecture> _items;
            Activity _context;

            public SpinnerAdapter(Activity context, List<Prefecture> items)
            {
                this._context = context;
                this._items = items ?? new List<Prefecture>();
            }
            public SpinnerAdapter(Activity context) : this(context, null) {; }

            public override Prefecture this[int position] => _items[position];
            public override int Count => _items.Count;
            public override long GetItemId(int position) => position;

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var item = _items[position];

                var view = convertView;
                if (view == null) view = _context.LayoutInflater.Inflate(Resource.Layout.list_item, null);
                ((TextView)view).Text = item.Name;

                return view;
            }

            public void Refresh(Prefecture[] items)
            {
                _items.Clear();
                _items.AddRange(items);
                this.NotifyDataSetChanged();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

