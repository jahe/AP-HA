using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace AP_HA
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!stackSlider.IsEnabled)
                return;

            int zoomSpeed = 3;
            stackSlider.Value += zoomSpeed * e.Delta / Math.Abs(e.Delta);
        }

        public void loadPicture(int picNo)
        {
            Stream imageStreamSource = new FileStream(pictureStack.getPictureFromList(picNo), FileMode.Open, FileAccess.Read, FileShare.Read);
            TiffBitmapDecoder decoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];

            imgControl.Source = bitmapSource;
            debugTxtBox.Text = pictureStack.getPictureFromList(picNo);
        }
    }
}
