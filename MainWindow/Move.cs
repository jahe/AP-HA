﻿using System;
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
        private Point? lastPoint;           // ? um den Point "nullable" zu machen

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
                        lastPoint = mousePos;
                        Mouse.Capture(scrollViewer);
                    }
                    break;

                case Tool.Pen:
                    penMouseLeftButtonDown(sender, e);
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
                    Point posNow = e.GetPosition(scrollViewer);

                    if (lastPoint != null)
                    {
                        double dX = posNow.X - lastPoint.Value.X;
                        double dY = posNow.Y - lastPoint.Value.Y;

                        lastPoint = posNow;

                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                        scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
                    }
                    break;

                case Tool.Pen:
                    penMouseMove(sender, e);
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
                    lastPoint = null;
                    break;

                case Tool.Pen:
                    penMouseLeftButtonUp(sender, e);
                    break;

                default:
                    break;
            }
        }
    }
}