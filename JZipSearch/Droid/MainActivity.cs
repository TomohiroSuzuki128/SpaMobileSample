using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Linq;
using System.Collections.Generic;
using JZipCodeSearchClient;
using Android.Views;
using Android.Content;
using System.Net;
using System.Threading.Tasks;

namespace JZipSearch.Droid
{
    [Activity(Label = "郵便番号検索", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var fromZipCodeTextEdit = FindViewById<EditText>(Resource.Id.fromZipCodeTextEdit);
            var fromZipCodeButton = FindViewById<Button>(Resource.Id.fromZipCodeButton);
            var fromAddressSpinner = FindViewById<Spinner>(Resource.Id.fromAddressSpinner);
            var fromAddressTextEdit = FindViewById<EditText>(Resource.Id.fromAddressTextEdit);
            var fromAddressButton = FindViewById<Button>(Resource.Id.fromAddressButton);
            var listView = FindViewById<ListView>(Resource.Id.listView1);

            var prefAddapter = new SpinnerAdapter(this, Prefectures.All().ToList());
            fromAddressSpinner.Adapter = prefAddapter;
            fromAddressSpinner.SetSelection(Prefectures.All().Select((pref, index) => new { Pref = pref, Index = index }).First(m => m.Pref.Name.Contains("東京")).Index);

            var listAdapter = new CustomListAdapter(this);
            listView.Adapter = listAdapter;

            fromZipCodeButton.Click += async (sender, e) =>
                {
                    var zipCode = fromZipCodeTextEdit.Text;
                    var addressList = await JZipSearch.Core.JZipSearchClient.ZipToAddress(zipCode);
                    listAdapter.Refresh(addressList);
                };

            fromAddressButton.Click += async (sender, e) =>
            {
                var address = fromAddressTextEdit.Text;
                var pref = prefAddapter[fromAddressSpinner.SelectedItemPosition].Code;
                var addressList = await JZipSearch.Core.JZipSearchClient.AddressToZip(pref, address);
                listAdapter.Refresh(addressList);
            };

            listView.ItemClick += (sender, e) =>
            {
                var address = listAdapter[e.Position];
                var addresText = $"{address.Prefecture}{address.City}{address.Machi}";
                var url = $"https://www.bing.com/search?q={WebUtility.UrlEncode(addresText)}";
                var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
                StartActivity(intent);
            };

            Task.Run(async () =>
            {
                try
                {
                    var prefectures = await JZipSearch.Core.JZipSearchClient.Prefectures();
                    prefAddapter.Refresh(prefectures);
                }
                finally
                {
                    ;
                }
            });
        }

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

        class CustomListAdapter : BaseAdapter<Address>
        {
            List<Address> _items;
            Activity _context;

            public CustomListAdapter(Activity context, List<Address> items)
            {
                this._context = context;
                this._items = items ?? new List<Address>();
            }
            public CustomListAdapter(Activity context) : this(context, null) {; }

            public override Address this[int position] => _items[position];
            public override int Count => _items.Count;
            public override long GetItemId(int position) => position;

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var item = _items[position];

                var view = convertView;
                if (view == null) view = _context.LayoutInflater.Inflate(Resource.Layout.list_item, null);
                ((TextView)view).Text = item.ToString();

                return view;
            }

            public void Refresh(Address[] addressList)
            {
                _items.Clear();
                _items.AddRange(addressList);
                this.NotifyDataSetChanged();
            }
        }
    }
}

