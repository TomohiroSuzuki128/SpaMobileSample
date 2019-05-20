using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace JZipSearch.iOSGawa.Views
{
    public partial class MainViewController : UIViewController
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeUI();

            var request = NSUrlRequest.FromUrl(NSUrl.FromString("https://decode19sap.azurewebsites.net/index.html"));
            webView.LoadRequest(request);
        }

    }
}