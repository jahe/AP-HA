using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace AP_HA
{
    public partial class MainWindow
    {
        bool penButtonDown = false;
        Stack<Polyline> polylineStack = new Stack<Polyline>();

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

            // active mark
            Mark activeMark = (Mark)marksListBox.SelectedItem;

            // create polyline
            Polyline myPolyline = new Polyline();

            myPolyline.Stroke = activeMark.BrushColor;
            myPolyline.StrokeThickness = Convert.ToInt32(penStrokeThickness.Text);
            myPolyline.FillRule = FillRule.EvenOdd;

            // add path
            polylineStack.Push(myPolyline);
            canvas.Children.Add(myPolyline);
        }

        private void penMouseMove(object sender, MouseEventArgs e)
        {
            if (!penButtonDown)
                return;

            // position
            Point canvasPos = e.GetPosition(canvas);

            // draw on active polyline
            Polyline activeLine = polylineStack.Peek();
            activeLine.Points.Add(canvasPos);
        }

        private void penMouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            penButtonDown = false;
        }
    }
}
