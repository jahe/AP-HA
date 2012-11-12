using System;
using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Console.WriteLine("Helligkeit: " + e.NewValue);
        }
    }
}