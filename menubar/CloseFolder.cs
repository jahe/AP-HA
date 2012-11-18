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
        private void closeFolder_Click(object sender, RoutedEventArgs e)
        {
            closeFolder();
        }

        private void closeFolder()
        {
            stackSlider.Value = 0;
            StackIsLoaded = false;
            imgControl.Source = null;
            debugTxtBox.Text = "Bitte einen Stapel öffnen";
            pictureStack.stackReset();
        }
    }
}
