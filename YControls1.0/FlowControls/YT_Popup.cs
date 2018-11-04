using System.Windows.Controls.Primitives;

namespace YControls.FlowControls {
    public class YT_Popup : Popup {
        #region Properties
        #endregion

        #region Methods
        private void UpdateLocation() {
            typeof(Popup).GetMethod("UpdatePosition",
                  System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(this, null);
        }
        #endregion

        #region Constructors

        #endregion
    }
}
