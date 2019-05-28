using System;
using UIKit;

namespace JZipSearch.iOS.Views
{
    public class UIPaddingTextFieldDelegate : UITextFieldDelegate
    {

        public override bool ShouldReturn(UITextField textField)
        {
            textField.ResignFirstResponder();
            return true;
        }

    }
}
