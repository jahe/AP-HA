using System;
using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        #region Properties
        private bool _userContrast = false;
        public bool UserContrast
        {
            get { return _userContrast; }

            set
            {
                _userContrast = value;
                loadPicture((int)stackSlider.Value);
                OnPropertyChanged("UserContrast");
            }
        }

        private double _imageContrast = 100;
        public double ImageContrast
        {
            get { return _imageContrast; }

            set
            {
                _imageContrast = value;

                if (StackIsLoaded)
                {
                    loadPicture((int)stackSlider.Value);
                }
            }
        }
        #endregion

        private void checkBoxContrast_Checked(object sender, RoutedEventArgs e)
        {
            UserContrast = true;
        }

        private void checkBoxContrast_Unchecked(object sender, RoutedEventArgs e)
        {
            UserContrast = false;
        }

        private void ContrastSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ImageContrast = ContrastSlider.Value;
        }
    }
}