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
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void closeFolder(object sender, RoutedEventArgs e)
        {
            stackSlider.Value = 0;
            stackSlider.IsEnabled = false;
            imgControl.Source = null;
            debugTxtBox.Text = null;
            pictureStack.stackReset();
        }
    }
}
