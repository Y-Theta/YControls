///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace TestCore {
    public class SingletonTest<T> where T : class,new() {
        #region Properties
        private static T _singleton;
        protected static object _lock = new object();
        public static T Singleton {
            get {
                if (_singleton == null)
                    lock (_lock) {
                        if (_singleton == null)
                            _singleton = new T();
                    }
                return _singleton;
            }
        }

        #endregion

        #region Methods
        public void Get() {
            Debug.WriteLine(_lock, GetHashCode().ToString());
        }
        #endregion

        #region Constructors
        #endregion
    }

}
