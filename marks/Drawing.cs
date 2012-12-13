using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (polylineStack.Count <= 0)
                return;

            Polyline lastPolyline = polylineStack.Pop();
            markCanvas.Children.Remove(lastPolyline);
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

            // add polyline
            int activeLayer = Convert.ToInt32(stackSlider.Value);

            activeMark.AddPolyline(activeLayer, myPolyline);
            polylineStack.Push(myPolyline);
            markCanvas.Children.Add(myPolyline);
        }

        private void penMouseMove(object sender, MouseEventArgs e)
        {
            if (!penButtonDown)
                return;

            // position
            Point canvasPos = e.GetPosition(markCanvas);

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
