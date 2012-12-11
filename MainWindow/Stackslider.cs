﻿using System;
using System.Windows;
using System.Windows.Input;

namespace AP_HA
{
    public partial class MainWindow
    {
        private int zoomSpeed = 1;

        private void stackSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                savePicture((int)e.OldValue);
                loadPicture((int)e.NewValue);
                displayLayerMarks((int)e.NewValue);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message+" \nDer aktuelle Stapel wird geschlossen");
                refreshSession();
            }
            
            if (stackSlider.Value == 0)
            {
                CutableLeft = false;
            }
            /**else if (stackSlider.Value == (double)pictureStack.PictureAmount - 1)
            {
                CutableRight = false;
            }**/
            else
            {
                CutableLeft = true;
                CutableRight = true;
            }
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!stackSlider.IsEnabled)
            {
                return;
            }
            stackSlider.Value += zoomSpeed * e.Delta / Math.Abs(e.Delta);
        }
    }
}
