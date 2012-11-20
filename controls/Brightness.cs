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
        #region Properties
        private bool _userBrightness = false;
        public bool UserBrightness
        {
            get { return _userBrightness; }

            set 
            {   _userBrightness = value;
                loadPicture((int)stackSlider.Value);
                OnPropertyChanged("UserBrightness");
            }
        }

        private int _imageBrightness = 100;
        public int ImageBrightness
        {
            get { return _imageBrightness; }

            set
            {
                _imageBrightness = value;

                if (StackIsLoaded)
                {
                    loadPicture((int)stackSlider.Value);
                    OnPropertyChanged("ImageBrightness");
                }                
            }
        }
        #endregion

        private void checkBoxBrightness_Checked(object sender, RoutedEventArgs e)
        {
            UserBrightness = true;
        }

        private void checkBoxBrightness_Unchecked(object sender, RoutedEventArgs e)
        {
            UserBrightness = false;
        }

        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ImageBrightness = (int)BrightnessSlider.Value;            
        }
    }
}