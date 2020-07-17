using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.Base {
    public class Class1 {


        public Class1() {
            List<string> res = new List<string>(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());
            res.ForEach(r => { Debug.WriteLine(r); });
            Debug.WriteLine(System.IO.Path.GetExtension("Res.BMP.BitmapPreview.bmp"));

            ToolboxBitmapAttribute tb = new ToolboxBitmapAttribute(typeof(MainWindow), "Res.BMP.BitmapPreview.bmp");
            //var smallImage = typeof(ToolboxBitmapAttribute).GetField("smallImage",BindingFlags.NonPublic|BindingFlags.Instance);
            var fields = typeof(ToolboxBitmapAttribute).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
        | BindingFlags.Static);
            fields.ToList().ForEach(f => { Debug.WriteLine(f.Name); });
            var smallImage = typeof(ToolboxBitmapAttribute).GetField("smallImage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
        | BindingFlags.Static);
            Image img = (Image)smallImage.GetValue(tb);

        }
    }
}
