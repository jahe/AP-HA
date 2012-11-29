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

namespace AP_HA
{
    public partial class MainWindow
    {
        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.ZoomIn;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.ZoomOut;
        }
    }
}
