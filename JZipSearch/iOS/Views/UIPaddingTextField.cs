using System;
using CoreGraphics;
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

    }
}