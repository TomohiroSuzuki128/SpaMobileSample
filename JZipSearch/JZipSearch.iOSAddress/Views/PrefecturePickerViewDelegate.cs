using System;
using System.Linq;
using JZipCodeSearchClient;
using UIKit;

namespace JZipSearch.iOSAddress.Views
{
    public class PrefecturePickerViewDelegate: UIPickerViewDelegate
    {

        // ドラムロールの各タイトル
        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            => Prefectures.All().Skip((int)row).FirstOrDefault().Name;

    }
}
