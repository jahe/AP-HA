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
    public partial class MainWindow
    {
        int zoomSpeed = 1;

        private void stackSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //try
            //{
                loadPicture((int)stackSlider.Value);
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message+" \nDer aktuelle Stapel wird geschlossen");
            //    refreshSession();
            //}
            
            if (stackSlider.Value == 0)
            {
                CutableLeft = false;
            }
            else if (stackSlider.Value == (double)pictureStack.PictureAmount - 1)
            {
                CutableRight = false;
            }
            else
            {
                CutableLeft = true;
                CutableRight = true;
            }
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!stackSlider.IsEnabled)
                return;

            stackSlider.Value += zoomSpeed * e.Delta / Math.Abs(e.Delta);
        }
    }
}
