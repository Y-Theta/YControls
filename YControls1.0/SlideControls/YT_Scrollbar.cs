using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace YControls.SlideControls {
    /// <summary>
    /// 自定义滚动条，暴露原属性
    /// </summary>
    public sealed class YT_Scrollbar : ScrollBar {
        #region Properties

        #region StylesOfTrackItems
        public Style IncreaseButtonVertival {
            get { return (Style)GetValue(IncreaseButtonVertivalProperty); }
            set { SetValue(IncreaseButtonVertivalProperty, value); }
        }
        public static readonly DependencyProperty IncreaseButtonVertivalProperty =
            DependencyProperty.Register("IncreaseButtonVertival", typeof(Style), 
                typeof(YT_Scrollbar), new PropertyMetadata(null));

        public Style ThumbVertival {
            get { return (Style)GetValue(ThumbVertivalProperty); }
            set { SetValue(ThumbVertivalProperty, value); }
        }
        public static readonly DependencyProperty ThumbVertivalProperty =
            DependencyProperty.Register("ThumbVertival", typeof(Style), 
                typeof(YT_Scrollbar), new PropertyMetadata(null));

        public Style DecreaseButtonVertival {
            get { return (Style)GetValue(DecreaseButtonVertivalProperty); }
            set { SetValue(DecreaseButtonVertivalProperty, value); }
        }
        public static readonly DependencyProperty DecreaseButtonVertivalProperty =
            DependencyProperty.Register("DecreaseButtonVertival", typeof(Style), 
                typeof(YT_Scrollbar), new PropertyMetadata(null));

        public Style IncreaseButtonHorizontal {
            get { return (Style)GetValue(IncreaseButtonHorizontalProperty); }
            set { SetValue(IncreaseButtonHorizontalProperty, value); }
        }
        public static readonly DependencyProperty IncreaseButtonHorizontalProperty =
            DependencyProperty.Register("IncreaseButtonHorizontal", typeof(Style),
                typeof(YT_Scrollbar), new PropertyMetadata(null));

        public Style ThumbHorizontal {
            get { return (Style)GetValue(ThumbHorizontalProperty); }
            set { SetValue(ThumbHorizontalProperty, value); }
        }
        public static readonly DependencyProperty ThumbHorizontalProperty =
            DependencyProperty.Register("ThumbHorizontal", typeof(Style),
                typeof(YT_Scrollbar), new PropertyMetadata(null));

        public Style DecreaseButtonHorizontal {
            get { return (Style)GetValue(DecreaseButtonHorizontalProperty); }
            set { SetValue(DecreaseButtonHorizontalProperty, value); }
        }
        public static readonly DependencyProperty DecreaseButtonHorizontalProperty =
            DependencyProperty.Register("DecreaseButtonHorizontal", typeof(Style),
                typeof(YT_Scrollbar), new PropertyMetadata(null));
        #endregion

        #endregion

        #region Methods
        #endregion

        #region Constructors
        public YT_Scrollbar() {
            
        }

        static YT_Scrollbar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_Scrollbar), new FrameworkPropertyMetadata(typeof(YT_Scrollbar)));
        }
        #endregion
    }

}