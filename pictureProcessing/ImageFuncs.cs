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
    public static class ImageFuncs
    {
        public static BitmapSource getImgFromPath(string path)
        {
            Stream imgStreamSource;
            TiffBitmapDecoder decoder;
            BitmapSource img;

            imgStreamSource = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            decoder = new TiffBitmapDecoder(imgStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            img = decoder.Frames[0];

            return img;
        }
    }
}
