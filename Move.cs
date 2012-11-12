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
        private Point? lastDragPoint;           // ? um den Point "nullable" zu machen

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.Move;
        }

        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (tool)
            {
                case Tool.Move:
                    Point mousePos = e.GetPosition(scrollViewer);

                    if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y < scrollViewer.ViewportHeight)
                    {
                        scrollViewer.Cursor = Cursors.SizeAll;
                        lastDragPoint = mousePos;
                        Mouse.Capture(scrollViewer);
                    }
                    break;

                default:
                    break;
            }
        }

        private void scrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            switch (tool)
            {
                case Tool.Move:
                    if (lastDragPoint != null)
                    {
                        Point posNow = e.GetPosition(scrollViewer);

                        double dX = posNow.X - lastDragPoint.Value.X;
                        double dY = posNow.Y - lastDragPoint.Value.Y;

                        lastDragPoint = posNow;

                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                        scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
                    }
                    break;

                default:
                    break;
            }
        }

        private void scrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (tool)
            {
                case Tool.Move:
                    scrollViewer.Cursor = Cursors.Arrow;
                    scrollViewer.ReleaseMouseCapture();
                    lastDragPoint = null;
                    break;

                default:
                    break;
            }
        }
    }
}
