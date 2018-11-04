using System;
using System.Globalization;
using System.Windows.Data;

namespace YControls.Converters {

    /// <summary>
    /// 字符相关转换器
    /// 用于将其它类型转换成字符
    /// 当parameter为空时，返回value
    /// 当parameter为字符串时 返回parameter格式化的value
    /// 
    /// </summary>
    public class StringConverter : IValueConverter {
        #region Properties

        private static readonly Lazy<StringConverter> _singleton = new Lazy<StringConverter>();

        public static StringConverter Singleton { get => _singleton.Value; }
        #endregion

        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            else if (parameter is string)
                return String.Format(parameter.ToString(), value);
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new ArgumentNullException("This is a one way converter");
        }
        #endregion

    }

}
