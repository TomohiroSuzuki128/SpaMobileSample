using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using JZipCodeSearchClient;
using UIKit;

namespace JZipSearch.iOS.Views
{
    public partial class MainViewController
    {
        static readonly nfloat fontSize = 20f;

        UILabel zipcodeLabel;
        UITextField zipcodeText;
        UIButton searchFromZipcodeButton;
        UIButton searchFromAddressButton;
        UILabel prefectureLabel;
        UITextField prefectureText;
        UIPickerView prefecturePicker;
        UILabel cityLabel;
        UILabel addressLabel;
        UITextView addressText;

        void InitializeUI()
        {
            View.ContentMode = UIViewContentMode.ScaleToFill;
            View.LayoutMargins = new UIEdgeInsets(0, 16, 0, 16);
            View.Frame = new CGRect(0, 0, 375, 667);
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth
                                    | UIViewAutoresizing.FlexibleHeight;

            zipcodeLabel = new UILabel
            {
                Frame = new CGRect(0, 0, 375, 20),
                Opaque = false,
                UserInteractionEnabled = false,
                ContentMode = UIViewContentMode.Left,
                Text = "郵便番号を入力",
                TextAlignment = UITextAlignment.Left,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 0,
                BaselineAdjustment = UIBaselineAdjustment.AlignBaselines,
                AdjustsFontSizeToFitWidth = false,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
            };
            View.AddSubview(zipcodeLabel);

            zipcodeLabel.HeightAnchor.ConstraintEqualTo(20).Active = true;
            zipcodeLabel.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            zipcodeLabel.TopAnchor.ConstraintEqualTo(View.TopAnchor, 100).Active = true;
            zipcodeLabel.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;
            zipcodeLabel.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

            zipcodeText = new UIPaddingTextField
            {
                Frame = new CGRect(0, 0, 375, 35),
                ContentMode = UIViewContentMode.ScaleToFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                KeyboardType = UIKeyboardType.Twitter,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "zipcodeText",
            };

            zipcodeText.Layer.BorderWidth = 1;
            zipcodeText.Layer.BorderColor = UIColor.LightGray.CGColor;

            View.AddSubview(zipcodeText);

            zipcodeText.HeightAnchor.ConstraintEqualTo(35f).Active = true;
            zipcodeText.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            zipcodeText.TopAnchor.ConstraintEqualTo(zipcodeLabel.BottomAnchor, 5).Active = true;
            zipcodeText.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;
            zipcodeText.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

            prefectureLabel = new UILabel
            {
                Frame = new CGRect(0, 0, 100, 20),
                Opaque = false,
                UserInteractionEnabled = false,
                ContentMode = UIViewContentMode.Left,
                Text = "都道府県",
                TextAlignment = UITextAlignment.Left,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 0,
                BaselineAdjustment = UIBaselineAdjustment.AlignBaselines,
                AdjustsFontSizeToFitWidth = false,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
            };
            View.AddSubview(prefectureLabel);

            prefectureLabel.HeightAnchor.ConstraintEqualTo(20).Active = true;
            prefectureLabel.WidthAnchor.ConstraintEqualTo(100).Active = true;

            prefectureLabel.TopAnchor.ConstraintEqualTo(zipcodeText.BottomAnchor, 15f).Active = true;
            prefectureLabel.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;

            prefectureText = new UIPaddingTextField
            {
                Frame = new CGRect(0, 0, 375, 35),
                ContentMode = UIViewContentMode.ScaleToFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                KeyboardType = UIKeyboardType.Twitter,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "prefectureText",
            };

            prefectureText.Layer.BorderWidth = 1;
            prefectureText.Layer.BorderColor = UIColor.LightGray.CGColor;

            // ピッカー
            prefecturePicker = new UIPickerView
            {
                Delegate = new PrefecturePickerViewDelegate(),
                DataSource = new PrefecturePickerViewDataSource(),
                ShowSelectionIndicator = true,
            };

            // 決定バー
            var toolbar = new UIToolbar(new CGRect(0, 0, View.Frame.Size.Width, 35));
            var spacelItem = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, this, null);
            var doneItem = new UIBarButtonItem(UIBarButtonSystemItem.Done,
                (s, e) =>
                {
                    prefectureText.EndEditing(true);
                    prefectureText.Text = Prefectures.All().Skip((int)prefecturePicker.SelectedRowInComponent(0)).FirstOrDefault().Name;
                });
            toolbar.SetItems(new UIBarButtonItem[] { spacelItem, doneItem }, true);

            prefectureText.InputView = prefecturePicker;
            prefectureText.InputAccessoryView = toolbar;

            View.AddSubview(prefectureText);

            prefectureText.HeightAnchor.ConstraintEqualTo(35f).Active = true;
            prefectureText.CenterYAnchor.ConstraintEqualTo(prefectureLabel.CenterYAnchor).Active = true;

            prefectureText.LeftAnchor.ConstraintEqualTo(prefectureLabel.RightAnchor, 20f).Active = true;
            prefectureText.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

        }



    }
}
