using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void btnPen_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.Pen;
        }

        private void btnEraser_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.ZoomOut;
        }
    }
}
