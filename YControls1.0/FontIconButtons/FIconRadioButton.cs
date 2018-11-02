using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace YControls.FontIconButtons {
    /// <summary>
    /// 从FIconToggleButton继承来的FIconRadioButton
    /// 基本与系统自带RadioButton功能一致
    /// </summary>
    public sealed class FIconRadioButton : FIconToggleButton {

        #region Properties
        [ThreadStatic]
        private static Hashtable _groupNameToElements;
        public static readonly DependencyProperty _currentlyRegisteredGroupName =
           DependencyProperty.Register("_currentlyRegisteredGroupName", typeof(string),
               typeof(FIconRadioButton), new PropertyMetadata(string.Empty));

        /// <summary>
        /// GroupName determine mutually excusive radiobutton groups
        /// </summary>
        [DefaultValue("")]
        [Localizability(LocalizationCategory.NeverLocalize)] // cannot be localized
        public string GroupName {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string),
                typeof(FIconRadioButton), new PropertyMetadata(string.Empty, OnGroupNameChanged));

        #endregion

        #region Method
        /// <summary>
        /// GroupName的变更回调
        /// </summary>
        private static void OnGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            FIconRadioButton radioButton = (FIconRadioButton)d;
            string groupName = e.NewValue as string;
            string currentlyRegisteredGroupName = (string)radioButton.GetValue(_currentlyRegisteredGroupName);
            if (groupName != currentlyRegisteredGroupName) {
                if (!string.IsNullOrEmpty(currentlyRegisteredGroupName))
                    Unregister(currentlyRegisteredGroupName, radioButton);
                if (!string.IsNullOrEmpty(groupName))
                    Register(groupName, radioButton);
            }
        }

        /// <summary>
        /// 注册一对RadioButton与GroupName的关联
        /// </summary>
        private static void Register(string groupName, FIconRadioButton radioButton) {
            if (_groupNameToElements == null)
                _groupNameToElements = new Hashtable(1);

            lock (_groupNameToElements) {
                ArrayList elements = (ArrayList)_groupNameToElements[groupName];

                if (elements == null) {
                    elements = new ArrayList(1);
                    _groupNameToElements[groupName] = elements;
                }
                else {
                    PurgeDead(elements, null);
                }

                elements.Add(new WeakReference(radioButton));
            }
            radioButton.SetValue(_currentlyRegisteredGroupName, groupName);
        }

        /// <summary>
        /// 注销一对RadioButton与GroupName的关联
        /// </summary>
        private static void Unregister(string groupName, FIconRadioButton radioButton) {
            if (_groupNameToElements == null)
                return;
            lock (_groupNameToElements) {
                ArrayList elements = (ArrayList)_groupNameToElements[groupName];

                if (elements != null) {
                    PurgeDead(elements, radioButton);
                    if (elements.Count == 0) {
                        _groupNameToElements.Remove(groupName);
                    }
                }
            }
            radioButton.SetValue(_currentlyRegisteredGroupName, null);
        }

        /// <summary>
        /// 在按钮组为空时清空键值
        /// </summary>
        private static void PurgeDead(ArrayList elements, object elementToRemove) {
            for (int i = 0; i < elements.Count;) {
                WeakReference weakReference = (WeakReference)elements[i];
                object element = weakReference.Target;
                if (element == null || element == elementToRemove) {
                    elements.RemoveAt(i);
                }
                else {
                    i++;
                }
            }
        }

        /// <summary>
        /// 通过反射获取保护函数
        /// </summary>
        private Visual GetVisualRoot (DependencyObject d) {
            var gvr = typeof(KeyboardNavigation).GetMethod("GetVisualRoot",
                  BindingFlags.NonPublic | BindingFlags.Static);
            return (Visual)gvr.Invoke(this, new object[] { d });
        }

        /// <summary>
        /// 更新关联的radiobutton
        /// </summary>
        private void UpdateRadioButtonGroup() {
            string groupName = GroupName;
            if (!string.IsNullOrEmpty(groupName)) {
                Visual rootScope = GetVisualRoot(this);
                if (_groupNameToElements == null)
                    _groupNameToElements = new Hashtable(1);
                lock (_groupNameToElements) {
                    ArrayList elements = (ArrayList)_groupNameToElements[groupName];
                    for (int i = 0; i < elements.Count;) {
                        WeakReference weakReference = (WeakReference)elements[i];
                        FIconRadioButton rb = weakReference.Target as FIconRadioButton;
                        if (rb == null) {
                            elements.RemoveAt(i);
                        }
                        else {
                            if (rb != this && (rb.IsChecked == true) && rootScope == GetVisualRoot(rb))
                                rb.UncheckRadioButton();
                            i++;
                        }
                    }
                }
            }
            else 
            {
                DependencyObject parent = this.Parent;
                if (parent != null) {
                    IEnumerable children = LogicalTreeHelper.GetChildren(parent);
                    IEnumerator itor = children.GetEnumerator();
                    while (itor.MoveNext()) {
                        FIconRadioButton rb = itor.Current as FIconRadioButton;
                        if (rb != null && rb != this && string.IsNullOrEmpty(rb.GroupName) && (rb.IsChecked == true))
                            rb.UncheckRadioButton();
                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UncheckRadioButton() {
            SetValue(IsCheckedProperty, false);
        }

        protected override void OnChecked(RoutedEventArgs e) {
            UpdateRadioButtonGroup();
            base.OnChecked(e);
        }

        protected override internal void OnToggle() {
            SetValue(IsCheckedProperty, true);
        }

        protected override void OnAccessKey(AccessKeyEventArgs e) {
            if (!IsKeyboardFocused) {
                Focus();
            }
            base.OnAccessKey(e);
        }
        #endregion

        #region Constructors

        #endregion
    }

}
