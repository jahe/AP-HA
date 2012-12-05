using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Windows;

namespace AP_HA
{
    public class ImageFuncs
    {
        public static BitmapSource getImgFromPath(string path)
        {
            BitmapSource img;
            Stream imgStreamSource = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            TiffBitmapDecoder decoder = new TiffBitmapDecoder(imgStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            img = decoder.Frames[0];               
            
            return img;           
        }
    }
}
