using System;
using System.Windows;
using System.Windows.Input;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void shootScChanged()
        {
            if (ShortCutChanged != null)
            {
                ShortCutEventArgs daten = new ShortCutEventArgs(sc);
                ShortCutChanged(this, daten);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            sc.register(e.Key);
            shootScChanged();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            sc.Remove(e.Key);
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            sc.register(e.ChangedButton);
            shootScChanged();
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            sc.Remove(e.ChangedButton);
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                sc.register(Wheel.Up);
            else if (e.Delta < 0)
                sc.register(Wheel.Down);
            else
                sc.register(Wheel.None);

            shootScChanged();

            sc.register(Wheel.None);
        }

        private Point? lastMovePoint;

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(this);

            if (lastMovePoint != null)
            {
                double deltaX = mousePos.X - lastMovePoint.Value.X;
                double deltaY = mousePos.Y - lastMovePoint.Value.Y;

                MouseMoveDirection moveDir = MouseMoveDirection.None;

                if (Math.Abs(deltaX) > Math.Abs(deltaY))        // Maus ist eher zur Seite gegangen
                {
                    if (deltaX < 0)
                        moveDir = MouseMoveDirection.Left;
                    else
                        moveDir = MouseMoveDirection.Right;
                }
                else if (Math.Abs(deltaX) < Math.Abs(deltaY))
                {
                    if (deltaY < 0)
                        moveDir = MouseMoveDirection.Up;
                    else
                        moveDir = MouseMoveDirection.Down;
                }

                sc.register(moveDir);
                shootScChanged();
                sc.register(MouseMoveDirection.None);
                lastMovePoint = mousePos;
            }
            else
            {
                lastMovePoint = mousePos;
            }
        }
    }
}
