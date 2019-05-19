﻿using Android.App;
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
    [Activity(Label = "郵便番号検索", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Main);

            var fromZipCodeTextEdit = FindViewById<EditText>(Resource.Id.fromZipCodeTextEdit);
            var fromZipCodeButton = FindViewById<Button>(Resource.Id.fromZipCodeButton);
            var fromAddressSpinner = FindViewById<Spinner>(Resource.Id.fromAddressSpinner);
            var fromAddressTextEdit = FindViewById<EditText>(Resource.Id.fromAddressTextEdit);

            var prefAddapter = new SpinnerAdapter(this, JZipSearch.Core.JZipSearchClient.SavedPrefectures()?.ToList());
            fromAddressSpinner.Adapter = prefAddapter;
            SetTokyo();

            fromZipCodeButton.Click += async (sender, e) =>
            {
                var zipCode = fromZipCodeTextEdit.Text;
                if (zipCode?.Any(c => !char.IsNumber(c)) == true)
                {
                    ShowToast("郵便番号は数字のみ入力してください.");
                    return;
                }
                if (zipCode?.Length != 7)
                {
                    ShowToast("郵便番号は7桁を入力してください.");
                    return;
                }
                var addressList = await JZipSearch.Core.JZipSearchClient.ZipToAddress(zipCode);
                if (addressList?.Any() == false) ShowNoItemToast();
                var address = addressList.FirstOrDefault();
                fromAddressSpinner.SetSelection(Prefectures.All().Select((pref, index) => new { Pref = pref, Index = index }).First(m => m.Pref.Name.Contains(address.Prefecture)).Index);
                fromAddressTextEdit.Text = $"{address.City}{address.Machi}";
            };

            Task.Run(async () =>
            {
                if (prefAddapter.Count > 0) return;
                var isRefreshed = await JZipSearch.Core.JZipSearchClient.RefreshSavedPrefectures();
                if (!isRefreshed) return;
                var savedPrefectures = JZipSearch.Core.JZipSearchClient.SavedPrefectures();
                this.RunOnUiThread(() =>
                {
                    ShowToast("都道府県リストが更新されました.");
                    prefAddapter.Refresh(savedPrefectures);
                    SetTokyo();
                });
            });
        }

        void ShowToast(string message)
        {
            var toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();
        }

        void ShowNoItemToast() => ShowToast("該当する情報が見つかりません.");

        void SetTokyo()
        {
            var fromAddressSpinner = FindViewById<Spinner>(Resource.Id.fromAddressSpinner);
            fromAddressSpinner.SetSelection(Prefectures.All().Select((pref, index) => new { Pref = pref, Index = index }).First(m => m.Pref.Name.Contains("東京")).Index);
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
    }
}

