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
        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(scrollViewer);
            double verticalShift = 0.0;
            double horizontalShift = 0.0;

            double mousePosOffsetX = mousePos.X * 100 / scrollViewer.ViewportWidth;
            double mousePosOffsetY = mousePos.Y * 100 / scrollViewer.ViewportHeight;

            verticalShift = mousePosOffsetY * 50 / 100;
            horizontalShift = mousePosOffsetX * 50 / 100;
            
            switch (tool)
            {
                case Tool.ZoomIn:
                    canvas.Height = canvas.ActualHeight + 50;
                    canvas.Width = canvas.ActualWidth + 50;

                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + horizontalShift);
                    scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + verticalShift);
                    break;
                case Tool.ZoomOut:
                    canvas.Height = canvas.ActualHeight - 50;
                    canvas.Width = canvas.ActualWidth - 50;
                    break;
            }
        }

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
