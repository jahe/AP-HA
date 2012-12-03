using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AP_HA
{
    public partial class MainWindow
    {
        bool penButtonDown = false;

        private void btnPen_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.Pen;
        }

        private void btnEraser_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.ZoomOut;
        }

        private void penMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            penButtonDown = true;
        }

        private void penMouseMove(object sender, MouseEventArgs e)
        {
            if (!penButtonDown)
                return;

            Point canvasPos = e.GetPosition(canvas);
            Console.WriteLine(canvasPos);
        }

        private void penMouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            penButtonDown = false;
        }
    }
}
