using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public void loadPicture(int picNo)
        {
            imageStreamSource = new FileStream(pictureStack.getPictureFromList(picNo), FileMode.Open, FileAccess.Read, FileShare.Read);
            decoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            bitmapSource = decoder.Frames[0];

            Bitmap temp = BitmapFromBitmapSource(bitmapSource);

            temp = AdjustBrightness(temp, ImageBrightness);

            imgControl.Source = BitmapSourceFromBitmap(temp);
            debugTxtBox.Text = pictureStack.getPictureFromList(picNo);
        }
    }
}
