using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YControls.Converters {

    /// <summary>
    /// Visibility相关转换器
    /// 用于将其它类型转换成Visibility
    /// 当value是字符类型时，则在字符不为空时返回可见
    /// </summary>
    public class VisibilityConverter : IValueConverter {
        #region Properties
        private static readonly Lazy<VisibilityConverter> _singleton = new Lazy<VisibilityConverter>();
        public static VisibilityConverter Singleton { get => _singleton.Value; }
        #endregion

        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null)
                return Visibility.Collapsed;
            else if (value is string)
                return string.IsNullOrWhiteSpace(value.ToString()) ? Visibility.Collapsed : Visibility.Visible;
            else if (value is bool) {
                if (parameter is null)
                    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                else
                    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (value is Enum) {
                return value.ToString().Equals(parameter.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new ArgumentNullException("This is a one way converter");
        }
        #endregion

    }

}
