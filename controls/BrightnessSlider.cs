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
using System.Drawing;

namespace AP_HA
{
    public partial class MainWindow
    {
        private int _imageBrightness = 100;
        public int ImageBrightness
        {
            get { return _imageBrightness; }
            set
            {
                _imageBrightness = value;
                //OnPropertyChanged("StackIsLoaded");
                if (StackIsLoaded)
                {
                    loadPicture((int)stackSlider.Value);
                }                
            }
        }

        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ImageBrightness = (int)BrightnessSlider.Value;            
        }
    }
}