using System;
using System.Collections.Generic;
using System.Linq;
using JZipCodeSearchClient;
using UIKit;

namespace JZipSearch.iOSAddress.Views
{
    public class PrefecturePickerViewDataSource : UIPickerViewDataSource
    {
        // ドラムロールの列数
        public override nint GetComponentCount(UIPickerView pickerView)
            => 1;

        // ドラムロールの行数
        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            => Prefectures.All().Count();
    }
}
