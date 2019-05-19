using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using JZipCodeSearchClient;
using UIKit;

namespace JZipSearch.iOSAddress.Views
{
    public partial class MainViewController
    {
        static readonly nfloat fontSize = 20f;

        static readonly nfloat labelHeight = 20f;
        static readonly nfloat labelWidth = 100f;

        static readonly nfloat textFieldHeight = 35f;

        static readonly nfloat addressRowSpace = 30f;
        static readonly nfloat addressColumnSpace = 20f;

        UILabel zipcodeLabel;
        UITextField zipcodeText;
        UIButton searchFromZipcodeButton;
        UIButton searchFromAddressButton;
        UILabel prefectureLabel;
        UITextField prefectureText;
        UIPickerView prefecturePicker;
        UILabel cityLabel;
        UITextField cityText;
        UILabel addressLabel;
        UITextField addressText;

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
                Frame = new CGRect(0, 0, 100, 20),
                Opaque = false,
                UserInteractionEnabled = false,
                ContentMode = UIViewContentMode.Left,
                Text = "郵便番号",
                TextAlignment = UITextAlignment.Left,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 0,
                BaselineAdjustment = UIBaselineAdjustment.AlignBaselines,
                AdjustsFontSizeToFitWidth = false,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
            };
            View.AddSubview(zipcodeLabel);

            zipcodeLabel.HeightAnchor.ConstraintEqualTo(labelHeight).Active = true;
            zipcodeLabel.WidthAnchor.ConstraintEqualTo(labelWidth).Active = true;

            zipcodeLabel.TopAnchor.ConstraintEqualTo(View.TopAnchor, 100).Active = true;
            zipcodeLabel.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;

            zipcodeText = new UIPaddingTextField
            {
                Frame = new CGRect(0, 0, 375, 35),
                ContentMode = UIViewContentMode.ScaleToFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                KeyboardType = UIKeyboardType.Twitter,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "zipcodeText",
                Placeholder = "郵便番号を入力",
            };

            zipcodeText.Layer.BorderWidth = 1;
            zipcodeText.Layer.BorderColor = UIColor.LightGray.CGColor;

            View.AddSubview(zipcodeText);

            zipcodeText.HeightAnchor.ConstraintEqualTo(textFieldHeight).Active = true;
            zipcodeText.CenterYAnchor.ConstraintEqualTo(zipcodeLabel.CenterYAnchor).Active = true;

            zipcodeText.LeftAnchor.ConstraintEqualTo(zipcodeLabel.RightAnchor, addressColumnSpace).Active = true;
            zipcodeText.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;


            searchFromZipcodeButton = new UIButton(UIButtonType.RoundedRect)
            {
                Frame = new CGRect(0, 0, 375, 20),
                Opaque = false,
                ContentMode = UIViewContentMode.ScaleToFill,
                HorizontalAlignment = UIControlContentHorizontalAlignment.Center,
                VerticalAlignment = UIControlContentVerticalAlignment.Center,
                LineBreakMode = UILineBreakMode.MiddleTruncation,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "searchFromZipcodeButton",
            };

            searchFromZipcodeButton.SetTitle("郵便番号から住所を検索", UIControlState.Normal);
            View.AddSubview(searchFromZipcodeButton);

            searchFromZipcodeButton.HeightAnchor.ConstraintEqualTo(40f).Active = true;
            searchFromZipcodeButton.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            searchFromZipcodeButton.TopAnchor.ConstraintEqualTo(zipcodeText.BottomAnchor, 15f).Active = true;
            searchFromZipcodeButton.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;
            searchFromZipcodeButton.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;


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

            prefectureLabel.HeightAnchor.ConstraintEqualTo(labelHeight).Active = true;
            prefectureLabel.WidthAnchor.ConstraintEqualTo(labelWidth).Active = true;

            prefectureLabel.TopAnchor.ConstraintEqualTo(searchFromZipcodeButton.BottomAnchor, 25f).Active = true;
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
            prefecturePicker.Select(
                nint.Parse(Prefectures.All().FirstOrDefault(x => x.Name == "東京都").Code) - 1,
                0, false);

            // 決定バー
            var toolBar = new UIToolbar
            {
                BarStyle = UIBarStyle.Default,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            toolBar.HeightAnchor.ConstraintEqualTo(40).Active = true;
            toolBar.WidthAnchor.ConstraintEqualTo(View.Frame.Width).Active = true;

            var spacelItem = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, this, null);
            var doneItem = new UIBarButtonItem(UIBarButtonSystemItem.Done,
                (s, e) =>
                {
                    prefectureText.EndEditing(true);
                    prefectureText.Text = Prefectures.All().Skip((int)prefecturePicker.SelectedRowInComponent(0)).FirstOrDefault().Name;
                });
            toolBar.SetItems(new UIBarButtonItem[] { spacelItem, doneItem }, true);

            prefectureText.InputView = prefecturePicker;
            prefectureText.InputAccessoryView = toolBar;

            View.AddSubview(prefectureText);

            prefectureText.HeightAnchor.ConstraintEqualTo(textFieldHeight).Active = true;
            prefectureText.CenterYAnchor.ConstraintEqualTo(prefectureLabel.CenterYAnchor).Active = true;

            prefectureText.LeftAnchor.ConstraintEqualTo(prefectureLabel.RightAnchor, addressColumnSpace).Active = true;
            prefectureText.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

            cityLabel = new UILabel
            {
                Frame = new CGRect(0, 0, 100, 20),
                Opaque = false,
                UserInteractionEnabled = false,
                ContentMode = UIViewContentMode.Left,
                Text = "市区町村",
                TextAlignment = UITextAlignment.Left,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 0,
                BaselineAdjustment = UIBaselineAdjustment.AlignBaselines,
                AdjustsFontSizeToFitWidth = false,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
            };
            View.AddSubview(cityLabel);

            cityLabel.HeightAnchor.ConstraintEqualTo(labelHeight).Active = true;
            cityLabel.WidthAnchor.ConstraintEqualTo(labelWidth).Active = true;

            cityLabel.TopAnchor.ConstraintEqualTo(prefectureLabel.BottomAnchor, addressRowSpace).Active = true;
            cityLabel.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;


            cityText = new UIPaddingTextField
            {
                Frame = new CGRect(0, 0, 375, 35),
                ContentMode = UIViewContentMode.ScaleToFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                KeyboardType = UIKeyboardType.Twitter,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "prefectureText",
            };

            cityText.Layer.BorderWidth = 1;
            cityText.Layer.BorderColor = UIColor.LightGray.CGColor;

            View.AddSubview(cityText);

            cityText.HeightAnchor.ConstraintEqualTo(textFieldHeight).Active = true;
            cityText.CenterYAnchor.ConstraintEqualTo(cityLabel.CenterYAnchor).Active = true;

            cityText.LeftAnchor.ConstraintEqualTo(cityLabel.RightAnchor, addressColumnSpace).Active = true;
            cityText.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

            addressLabel = new UILabel
            {
                Frame = new CGRect(0, 0, 100, 20),
                Opaque = false,
                UserInteractionEnabled = false,
                ContentMode = UIViewContentMode.Left,
                Text = "番地",
                TextAlignment = UITextAlignment.Left,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 0,
                BaselineAdjustment = UIBaselineAdjustment.AlignBaselines,
                AdjustsFontSizeToFitWidth = false,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
            };
            View.AddSubview(addressLabel);

            addressLabel.HeightAnchor.ConstraintEqualTo(labelHeight).Active = true;
            addressLabel.WidthAnchor.ConstraintEqualTo(labelWidth).Active = true;

            addressLabel.TopAnchor.ConstraintEqualTo(cityLabel.BottomAnchor, addressRowSpace).Active = true;
            addressLabel.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;


            addressText = new UIPaddingTextField
            {
                Frame = new CGRect(0, 0, 375, 35),
                ContentMode = UIViewContentMode.ScaleToFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                KeyboardType = UIKeyboardType.Twitter,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "prefectureText",
            };

            addressText.Layer.BorderWidth = 1;
            addressText.Layer.BorderColor = UIColor.LightGray.CGColor;

            View.AddSubview(addressText);

            addressText.HeightAnchor.ConstraintEqualTo(textFieldHeight).Active = true;
            addressText.CenterYAnchor.ConstraintEqualTo(addressLabel.CenterYAnchor).Active = true;

            addressText.LeftAnchor.ConstraintEqualTo(addressLabel.RightAnchor, addressColumnSpace).Active = true;
            addressText.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;


        }



    }
}
