///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace YControlCore.Converter {

    /// <summary>
    /// 用于转化bool类型值
    /// </summary>
    public class BooleanConverter : SingletonConverterBase<BooleanConverter> {
        #region Properties
        #endregion

        #region Methods
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Debug.WriteLine(value);
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructors
        #endregion

    }
}
