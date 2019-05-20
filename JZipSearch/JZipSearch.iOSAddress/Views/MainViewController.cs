using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using JZipCodeSearchClient;
using JZipSearch.Core;
using UIKit;

namespace JZipSearch.iOSAddress.Views
{
    public partial class MainViewController : UIViewController
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeUI();
            searchFromZipcodeButton.AddTarget(this, new ObjCRuntime.Selector("zipCodeEvent:"), UIControlEvent.TouchUpInside);
        }

        [Export("zipCodeEvent:")]
        async void ZipCodeEvent(NSObject sender)
        {
            var zipCode = zipcodeText.Text.Replace("-", "");

            if (string.IsNullOrWhiteSpace(zipCode))
            {
                PresentAlert("郵便番号を入力してください。");
                return;
            }

            if (zipCode?.Any(c => !char.IsNumber(c)) == true)
            {
                PresentAlert("郵便番号は数字のみ入力してください。");
                return;
            }

            var addressList = await JZipSearchClient.ZipToAddress(zipCode);

            if (addressList?.Any() == false)
                PresentAlert("該当する情報が見つかりません.");

            prefectureText.Text = addressList.FirstOrDefault().Prefecture;
            cityText.Text = addressList.FirstOrDefault().City;
            addressText.Text = addressList.FirstOrDefault().Machi;

            prefecturePicker.Select(
            nint.Parse(Prefectures.All().FirstOrDefault(x => x.Name == addressList.FirstOrDefault().Prefecture).Code) - 1,
            0, false);

        }

        void PresentAlert(string message)
        {
            var alertController = UIAlertController.Create(string.Empty, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("閉じる", UIAlertActionStyle.Cancel, null));
            PresentViewController(alertController, true, null);
        }

    }
}