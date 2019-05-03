using System;
using CoreGraphics;
using UIKit;

namespace JZipSearch.iOS
{
    public class UIPaddingTextField : UITextField
    {
        CGPoint padding = new CGPoint(6.0, 0.0);

        public override CGRect TextRect(CGRect forBounds)
        {
            // 通常
            return forBounds.Inset(padding.X, padding.Y);
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            // 入力中
            return forBounds.Inset(padding.X, padding.Y);
        }

        public override CGRect PlaceholderRect(CGRect forBounds)
        {
            // プレースホルダー
            return forBounds.Inset(padding.X, padding.Y);
        }

    }
}