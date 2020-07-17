///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace YControlCore.Converter {
    public abstract class SingletonConverterBase<T>: IValueConverter  where T : class, new () {
        #region Properties

        private static T _singleton;
        private static readonly string _lock = typeof(T).ToString();
        public static T Singleton {
            get {
                if(_singleton == null)
                    lock (_lock) {
                        if (_singleton == null)
                            _singleton = new T();
                    }
                return _singleton;
            }
        }

        #endregion

        #region Methods
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        #endregion

    }
}
