using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YControls.Converters {
    /// <summary>
    /// 数字相关转换器
    /// 用于将其它类型转换成数字类型,或需要根据value的值动态计算
    /// 
    /// </summary>
    public class NumberConverter : IValueConverter {
        #region Properties

        private static readonly Lazy<NumberConverter> _singleton = new Lazy<NumberConverter>();

        public static NumberConverter Singleton { get => _singleton.Value; }
        #endregion

        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (targetType == typeof(double)) {
                return null;
            }
            else if (targetType == typeof(int)) {
                return null;
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new ArgumentNullException("This is a one way converter");
        }
        #endregion

    }

}
