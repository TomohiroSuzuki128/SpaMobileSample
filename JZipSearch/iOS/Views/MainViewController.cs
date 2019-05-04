using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using JZipCodeSearchClient;
using UIKit;

namespace JZipSearch.iOS.Views
{
    public partial class MainViewController : UIViewController
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeUI();
        }


        void SetVirticalOffset(UITextView textView)
        {
            nfloat topOffset = (textView.Bounds.Size.Height - textView.ContentSize.Height * textView.ZoomScale) / 2.0f;
            topOffset = topOffset < 0.0f ? 0.0f : topOffset;
            textView.ContentOffset = new CGPoint(textView.ContentOffset.X, textView.ContentOffset.Y - topOffset);
        }

    }
}