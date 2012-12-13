using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AP_HA
{
    public partial class MainWindow
    {
        private Point? lastPoint;           // ? um den Point "nullable" zu machen

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.Move;
        }

        private void scrollViewerClick(object sender, MouseButtonEventArgs e)
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

                //case Tool.Pen:
                //    penMouseLeftButtonDown(sender, e);
                //    break;

                case Tool.CropSize:
                    tool = Tool.None;
                    break;

                default:
                    break;
            }
        }

        private void canvasClick(object sender, MouseButtonEventArgs e)
        {
            switch (tool)
            {
                //case Tool.Move:
                //    Point mousePos = e.GetPosition(scrollViewer);

                //    if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y < scrollViewer.ViewportHeight)
                //    {
                //        scrollViewer.Cursor = Cursors.SizeAll;
                //        lastPoint = mousePos;
                //        Mouse.Capture(scrollViewer);
                //    }
                //    break;

                case Tool.Pen:
                    penMouseLeftButtonDown(sender, e);
                    break;

                case Tool.CropLocation:
                    tool = Tool.None;
                    cropRectangle.Cursor = Cursors.Arrow;
                    break;

                default:
                    break;
            }
        }

        private void scrollViewerMove(object sender, MouseEventArgs e)
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

                //case Tool.Pen:
                //    penMouseMove(sender, e);
                //    break;

                case Tool.CropLocation:
                    Point mousePos2 = e.GetPosition(imgControl);
                    Canvas.SetLeft(cropRectangle, mousePos2.X);
                    Canvas.SetTop(cropRectangle, mousePos2.Y);
                    break;

                default:
                    break;
            }
        }

        private void canvasMove(object sender, MouseEventArgs e)
        {
            switch (tool)
            {
                //case Tool.Move:
                //    Point posNow = e.GetPosition(scrollViewer);

                //    if (lastPoint != null)
                //    {
                //        double dX = posNow.X - lastPoint.Value.X;
                //        double dY = posNow.Y - lastPoint.Value.Y;

                //        lastPoint = posNow;

                //        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                //        scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
                //    }
                //    break;

                case Tool.Pen:
                    penMouseMove(sender, e);
                    break;

                case Tool.CropSize:
                    Point mousePos3 = e.GetPosition(imgControl);

                    double width = mousePos3.X - Canvas.GetLeft(cropRectangle) + 13;

                    if (width > 100)
                    {
                        cropRectangle.Width = width;
                    }
                    double height = mousePos3.Y - Canvas.GetTop(cropRectangle) + 7;

                    if (height > 100)
                    {
                        cropRectangle.Height = height;
                    }
                    break;

                default:
                    break;
            }
        }

        private void scrollViewerButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (tool)
            {
                case Tool.Move:
                    scrollViewer.Cursor = Cursors.Arrow;
                    scrollViewer.ReleaseMouseCapture();
                    lastPoint = null;
                    break;

                default:
                    break;
            }
        }

        private void canvasButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (tool)
            {
                //case Tool.Move:
                //    scrollViewer.Cursor = Cursors.Arrow;
                //    scrollViewer.ReleaseMouseCapture();
                //    lastPoint = null;
                //    break;

                case Tool.Pen:
                    penMouseLeftButtonUp(sender, e);
                    break;

                default:
                    break;
            }
        }
    }
}
