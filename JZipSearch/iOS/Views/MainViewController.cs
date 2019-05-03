using System;
using CoreGraphics;
using UIKit;

namespace JZipSearch.iOS
{
    public class MainViewController : UIViewController
    {
        static readonly nfloat fontSize = 20f;

        UILabel zipcodeLabel;
        UITextField zipcodeText;
        UIButton searchFromZipcodeButton;
        UIButton searchFromAddressButton;
        UILabel prefectureLabel;
        UIPickerView prefecturePicker;
        UILabel cityLabel;
        UILabel addressLabel;
        UITextView addressText;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeUI();
        }

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

            prefecturePicker = new UIPickerView
            {
                Frame = new CGRect(0, 0, 375, 35),
                ContentMode = UIViewContentMode.ScaleToFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                //Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "prefecturePicker",
            };

            prefecturePicker.Layer.BorderWidth = 1;
            prefecturePicker.Layer.BorderColor = UIColor.LightGray.CGColor;

            View.AddSubview(prefecturePicker);

            prefecturePicker.HeightAnchor.ConstraintEqualTo(35f).Active = true;
            prefecturePicker.CenterYAnchor.ConstraintEqualTo(prefectureLabel.CenterYAnchor).Active = true;

            prefecturePicker.LeftAnchor.ConstraintEqualTo(prefectureLabel.RightAnchor, 20f).Active = true;
            prefecturePicker.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;





        }

        void SetVirticalOffset(UITextView textView)
        {
            nfloat topOffset = (textView.Bounds.Size.Height - textView.ContentSize.Height * textView.ZoomScale) / 2.0f;
            topOffset = topOffset < 0.0f ? 0.0f : topOffset;
            textView.ContentOffset = new CGPoint(textView.ContentOffset.X, textView.ContentOffset.Y - topOffset);
        }

    }
}
