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
using Microsoft.Win32;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void cutAfterCursor(object sender, RoutedEventArgs e)     //Menü->Bearbeiten->Stapel beschneiden->Bilder nach cursor
        {
            stackSlider.Maximum = stackSlider.Value;
            menuBackToOriginalCut.IsEnabled = true;
            stackCutted(true);
        }
    }
}
