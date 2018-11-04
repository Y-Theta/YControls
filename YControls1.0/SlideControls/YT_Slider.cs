using System.Windows;
using System.Windows.Controls;

namespace YControls.SlideControls {
    public sealed class YT_Slider : Slider {

        #region Properties

        public Style IncreaseButtonVertival {
            get { return (Style)GetValue(IncreaseButtonVertivalProperty); }
            set { SetValue(IncreaseButtonVertivalProperty, value); }
        }
        public static readonly DependencyProperty IncreaseButtonVertivalProperty =
            DependencyProperty.Register("IncreaseButtonVertival", typeof(Style),
                typeof(YT_Slider), new PropertyMetadata(null));

        public Style ThumbVertival {
            get { return (Style)GetValue(ThumbVertivalProperty); }
            set { SetValue(ThumbVertivalProperty, value); }
        }
        public static readonly DependencyProperty ThumbVertivalProperty =
            DependencyProperty.Register("ThumbVertival", typeof(Style),
                typeof(YT_Slider), new PropertyMetadata(null));

        public Style DecreaseButtonVertival {
            get { return (Style)GetValue(DecreaseButtonVertivalProperty); }
            set { SetValue(DecreaseButtonVertivalProperty, value); }
        }
        public static readonly DependencyProperty DecreaseButtonVertivalProperty =
            DependencyProperty.Register("DecreaseButtonVertival", typeof(Style),
                typeof(YT_Slider), new PropertyMetadata(null));

        public Style IncreaseButtonHorizontal {
            get { return (Style)GetValue(IncreaseButtonHorizontalProperty); }
            set { SetValue(IncreaseButtonHorizontalProperty, value); }
        }
        public static readonly DependencyProperty IncreaseButtonHorizontalProperty =
            DependencyProperty.Register("IncreaseButtonHorizontal", typeof(Style),
                typeof(YT_Slider), new PropertyMetadata(null));

        public Style ThumbHorizontal {
            get { return (Style)GetValue(ThumbHorizontalProperty); }
            set { SetValue(ThumbHorizontalProperty, value); }
        }
        public static readonly DependencyProperty ThumbHorizontalProperty =
            DependencyProperty.Register("ThumbHorizontal", typeof(Style),
                typeof(YT_Slider), new PropertyMetadata(null));

        public Style DecreaseButtonHorizontal {
            get { return (Style)GetValue(DecreaseButtonHorizontalProperty); }
            set { SetValue(DecreaseButtonHorizontalProperty, value); }
        }
        public static readonly DependencyProperty DecreaseButtonHorizontalProperty =
            DependencyProperty.Register("DecreaseButtonHorizontal", typeof(Style),
                typeof(YT_Slider), new PropertyMetadata(null));

        public Visibility ThumbToolTipVisiblity {
            get { return (Visibility)GetValue(ThumbToolTipVisiblityProperty); }
            set { SetValue(ThumbToolTipVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ThumbToolTipVisiblityProperty =
            DependencyProperty.Register("ThumbToolTipVisiblity", typeof(Visibility), 
                typeof(YT_Slider), new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region Methods
        #endregion

        #region Constructors
        static YT_Slider() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_Slider), new FrameworkPropertyMetadata(typeof(YT_Slider)));
        }
        #endregion
    }

}
