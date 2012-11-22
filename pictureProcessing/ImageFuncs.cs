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
        // --------- Adjust-Functions ------------

        public static Bitmap AdjustBrightness(Bitmap Image, int Value)
        {
            float FinalValue = (float)Value / 255.0f;

            ColorMatrix TempMatrix = new ColorMatrix();

            TempMatrix.Matrix = new float[][]{
                                new float[] {1, 0, 0, 0, 0},
                                new float[] {0, 1, 0, 0, 0},
                                new float[] {0, 0, 1, 0, 0},
                                new float[] {0, 0, 0, 1, 0},
                                new float[] {FinalValue, FinalValue, FinalValue, 1, 1} };

            return TempMatrix.Apply(Image);
        }

        public static Bitmap AdjustContrast(Bitmap Image, double value)
        {
            float FinalValue = (float)(1 - value) / 2;
            float tempValue = (float)value;
            ColorMatrix TempMatrix = new ColorMatrix();

            TempMatrix.Matrix = new float[][]{
                                new float[] {tempValue, 0, 0, 0, 0},
                                new float[] {0, tempValue, 0, 0, 0},
                                new float[] {0, 0, tempValue, 0, 0},
                                new float[] {0, 0, 0, tempValue, 0},
                                new float[] {FinalValue, FinalValue, FinalValue, 1, 1} };

            return TempMatrix.Apply(Image);
        }

        // ----------------------------------------
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

        public static Bitmap BitmapFromBitmapSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();

                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }

            return bitmap;
        }

        public static BitmapSource BitmapSourceFromBitmap(Bitmap bitmap)
        {
            BitmapSource bitSrc = null;
            var hBitmap = bitmap.GetHbitmap();

            bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return bitSrc;
        }
    }
}
