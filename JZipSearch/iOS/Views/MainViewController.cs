using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using JZipCodeSearchClient;
using JZipSearch.Core;
using UIKit;

namespace JZipSearch.iOS.Views
{
    public partial class MainViewController : UIViewController
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeUI();
            searchFromZipcodeButton.AddTarget(this, new ObjCRuntime.Selector("ZipCodeEvent:"), UIControlEvent.TouchUpInside);
            //searchFromZipcodeButton.AddTarget(this, new ObjCRuntime.Selector("NoResultEvent:"), UIControlEvent.TouchUpInside);
        }

        [Export("ZipCodeEvent:")]
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
            //listAdapter.Refresh(addressList);

            if (addressList?.Any() == false)
                PresentAlert("該当する情報が見つかりません.");
        }

        //[Export("NoResultEvent:")]
        //void NoResultEvent(NSObject sender)
        //{

        //}

        void SetVirticalOffset(UITextView textView)
        {
            nfloat topOffset = (textView.Bounds.Size.Height - textView.ContentSize.Height * textView.ZoomScale) / 2.0f;
            topOffset = topOffset < 0.0f ? 0.0f : topOffset;
            textView.ContentOffset = new CGPoint(textView.ContentOffset.X, textView.ContentOffset.Y - topOffset);
        }

        void PresentAlert(string message)
        {
            var alertController = UIAlertController.Create(string.Empty, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("閉じる", UIAlertActionStyle.Cancel, null));
            PresentViewController(alertController, true, null);
        }

    }
}