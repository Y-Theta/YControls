using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YControls.Converters {

    /// <summary>
    /// StringConverter 的参数 主要用于ConverterParameter需要动态绑定的情形
    /// </summary>
    public class StringConverterArgs :DependencyObject  {

        public string Format { get; set; }

        public bool FindRes { get; set; }

        public ResourceDictionary ResourceDic {
            get { return (ResourceDictionary)GetValue(ResourceDicProperty); }
            set { SetValue(ResourceDicProperty, value); }
        }
        public static readonly DependencyProperty ResourceDicProperty =
            DependencyProperty.Register("ResourceDic", typeof(ResourceDictionary),
                typeof(StringConverterArgs), new PropertyMetadata(null));

    }

    /// <summary>
    /// 字符相关转换器
    /// 用于将其它类型转换成字符
    /// 当parameter为空时，返回value
    /// 当parameter为字符串时 返回parameter格式化的value
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
            else if (parameter is StringConverterArgs arg) {
                if (arg.FindRes && arg.ResourceDic != null)
                    return String.Format(arg.ResourceDic[arg.Format] as string, value);
                return String.Format(arg.Format, value);
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
