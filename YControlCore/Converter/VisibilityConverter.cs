///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace YControlCore.Converter {
    /// <summary>
    /// Visibility相关转换器
    /// 用于将其它类型转换成Visibility
    /// 当value是字符类型时，则在字符不为空时返回可见
    /// </summary>
    public class VisibilityConverter : SingletonConverterBase<VisibilityConverter> {

        #region Methods
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null)
                return Visibility.Collapsed;
            else if (value is string)
                return string.IsNullOrWhiteSpace(value.ToString()) ? Visibility.Collapsed : Visibility.Visible;
            else if (value is bool) {
                if (parameter is null)
                    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                else if (parameter is string para) {
                    switch (para) {
                        case "YH":
                            return (bool)value ? Visibility.Visible : Visibility.Hidden;
                        default: return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                    }
                } else
                    return Visibility.Visible;
            } else if (value is Enum) {
                return value.ToString().Equals(parameter.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            } else
                return Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new ArgumentNullException("This is a one way converter");
        }
        #endregion

    }
}
