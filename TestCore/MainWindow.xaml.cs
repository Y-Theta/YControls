using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Image = System.Drawing.Image;
using Size = System.Drawing.Size;

using YControlCore.WindowBase;
using System.Text.RegularExpressions;

namespace TestCore {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Image largeImage;
        private static readonly Size largeSize = new Size(32, 32);

        private static string _suffix;
        internal static string Suffix {
            get {
                if (_suffix == null) {
                    _suffix = string.Empty;
                    var section = ConfigurationManager.GetSection("system.drawing") as SystemDrawingSection;
                    if (section != null) {
                        var value = section.BitmapSuffix;
                        if (value != null && value is string) {
                            _suffix = (string)value;
                        }
                    }
                }
                return _suffix;
            }
            set {
                // So unit tests can clear the cached suffix
                _suffix = value;
            }
        }

        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        private Image GetBitmapFromResource(Type t, string bitmapname) {
            if (bitmapname == null) {
                return null;
            }

            Image img = null;

            // load the image from the manifest resources. 
            //
            Stream stream = t.Module.Assembly.GetManifestResourceStream(t, bitmapname);
            if (stream != null) {
                Bitmap b = new Bitmap(stream);
                img = b;

            }
            return img;
        }
        public MainWindow() {
            InitializeComponent();

            string test = File.ReadAllText(@"C:\Users\Y_Theta\Desktop\Untitled-1.txt");
            //Debug.WriteLine(test);
            Regex pattern = new Regex("href=\"(/.+/(.+?\\.(ttf|woff)2*))\"");
            var collection = pattern.Matches(test);
            Debug.WriteLine(collection[0].Groups[2].Value);

            App.Current.Shutdown();
            //typeof(SystemParameters).GetProperties(BindingFlags.Static | BindingFlags.Public).ToList().ForEach(prop => {
            //    object val = null;

            //    Debug.WriteLine($" name - {prop.Name.PadRight(48)}  value - {prop.GetValue(null)}");
            //});


            //    List<string> res = new List<string>(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());
            //    res.ForEach(r => { Debug.WriteLine(r); });
            //    Debug.WriteLine(System.IO.Path.GetExtension("Res.BMP.BitmapPreview.bmp"));

            //    ToolboxBitmapAttribute tb = new ToolboxBitmapAttribute(typeof(MainWindow), "Res.BMP.BitmapPreview.bmp");
            //    //var smallImage = typeof(ToolboxBitmapAttribute).GetField("smallImage",BindingFlags.NonPublic|BindingFlags.Instance);
            //    var fields = typeof(ToolboxBitmapAttribute).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            //| BindingFlags.Static);
            //    fields.ToList().ForEach(f => { Debug.WriteLine(f.Name); });
            //    var smallImage = typeof(ToolboxBitmapAttribute).GetField("_smallImage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            //| BindingFlags.Static);
            //    Image img = (Image)smallImage.GetValue(tb);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }
    }
}
