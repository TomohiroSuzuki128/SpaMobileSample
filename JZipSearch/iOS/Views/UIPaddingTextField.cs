using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace JZipSearch.iOS
{
    public class UIPaddingTextField : UITextField
    {
        CGPoint padding = new CGPoint(6.0, 0.0);

        // 通常
        public override CGRect TextRect(CGRect forBounds)
            => forBounds.Inset(padding.X, padding.Y);

        // 入力中
        public override CGRect EditingRect(CGRect forBounds)
            => forBounds.Inset(padding.X, padding.Y);

        // プレースホルダー
        public override CGRect PlaceholderRect(CGRect forBounds)
            => forBounds.Inset(padding.X, padding.Y);

        public UIPaddingTextField(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public UIPaddingTextField(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public UIPaddingTextField()
        {
            Initialize();
        }

        void Initialize()
        {
            BorderStyle = UITextBorderStyle.None;
            Layer.CornerRadius = 5;
            Layer.BorderColor = UIColor.LightGray.CGColor;
            Layer.BorderWidth = 1;
            Layer.MasksToBounds = true;
        }

    }
}