using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter = System.Convert;
using ColorD = System.Drawing.Color;
using System.Windows.Data;
using System.Windows.Media;

namespace YControls.Converters {
    public class ColorConverter : IValueConverter {
        #region Properties

        private static readonly Lazy<ColorConverter> _singleton = new Lazy<ColorConverter>();

        public static ColorConverter Singleton { get => _singleton.Value; }
        #endregion

        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (targetType == typeof(Color)) {
                if (value is SolidColorBrush)
                    return ((SolidColorBrush)value).Color;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new ArgumentNullException("This is a one way converter");
        }
        #endregion
    }

}
