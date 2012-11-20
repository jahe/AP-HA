using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace AP_HA
{
    public partial class MainWindow
    {
        private BitmapSource BitmapSourceFromBitmap(Bitmap bitmap)
        {
            BitmapSource bitSrc = null;
            var hBitmap = bitmap.GetHbitmap();

            bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            return bitSrc;
        }
    }
}
