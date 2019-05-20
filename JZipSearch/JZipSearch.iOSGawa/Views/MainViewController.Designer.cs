using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using UIKit;
using WebKit;

namespace JZipSearch.iOSGawa.Views
{
    public partial class MainViewController
    {

        WKWebView webView;

        void InitializeUI()
        {
            View.ContentMode = UIViewContentMode.ScaleToFill;
            View.LayoutMargins = new UIEdgeInsets(0, 16, 0, 16);
            View.Frame = new CGRect(0, 0, 375, 667);
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth
                                    | UIViewAutoresizing.FlexibleHeight;



            var config = new WKWebViewConfiguration();
            var wKNavigationDelegate = new WKNavigationDelegate();

            webView = new WKWebView(View.Frame, config)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                NavigationDelegate = wKNavigationDelegate,
            };

            View.AddSubview(webView);

            webView.HeightAnchor.ConstraintEqualTo(View.HeightAnchor).Active = true;
            webView.WidthAnchor.ConstraintEqualTo(View.WidthAnchor).Active = true;

            webView.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
            webView.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor).Active = true;
        }

    }
}
