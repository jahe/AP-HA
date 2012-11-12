using System;
using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void ContrastSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Console.WriteLine("Kontrast: " + e.NewValue);
        }
    }
}