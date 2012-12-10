﻿using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void btnCrop_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.Crop;
            if (cropRectangle.Visibility != Visibility.Visible)
            {
                cropRectangle.Visibility = Visibility.Visible;
            }
            else
            {
                cropRectangle.Visibility = Visibility.Collapsed;
            }
                    
        } 
    }
}