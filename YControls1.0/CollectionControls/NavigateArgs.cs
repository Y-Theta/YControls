using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YControls.CollectionControls {
    /// <summary>
    /// 导航命令的参数，用于将Frame的行为与页面分离
    /// </summary>
    public class NavigateArgs {
        #region Properties
        public bool UseUri { get; set; }

        public object Content { get; set; }

        public Uri ContentUri { get; set; }

        public object ExtraData { get; set; }
        #endregion

        #region Constructors
        public NavigateArgs(object content, object extraData = null) {
            Content = content;
            ExtraData = extraData;
            UseUri = false;
        }

        public NavigateArgs(Uri contentUri, object extraData = null) {
            ContentUri = contentUri;
            ExtraData = extraData;
            UseUri = true;
        }
        #endregion
    }

}
