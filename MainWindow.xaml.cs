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
        enum Tool { ZoomIn, ZoomOut };
        private Tool tool;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void loadPicture(int picNo)
        {
            Stream imageStreamSource = new FileStream(pictureStack.getPictureFromList(picNo), FileMode.Open, FileAccess.Read, FileShare.Read);
            TiffBitmapDecoder decoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];

            imgControl.Source = bitmapSource;
            debugTxtBox.Text = pictureStack.getPictureFromList(picNo);
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (tool)
            {
                case Tool.ZoomIn:
                    canvas.Height = canvas.ActualHeight + 50;
                    canvas.Width = canvas.ActualWidth + 50;
                    break;
                case Tool.ZoomOut:
                    canvas.Height = canvas.ActualHeight - 50;
                    canvas.Width = canvas.ActualWidth - 50;
                    break;
            }

        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.ZoomIn;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.ZoomOut;
        }
    }
}
