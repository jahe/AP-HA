using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;

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
            this.debugTxtBox.Text = e.Key.ToString();

            sc.register(e.Key);

            Debug.WriteLine(e.Key.ToString() + " wurde gedrückt");

            shootScChanged();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            sc.Remove(e.Key);
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.debugTxtBox.Text = e.ChangedButton.ToString();

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
        private int moveCounter = 0;

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (moveCounter < 20)
                moveCounter++;
            else
            {
                moveCounter = 0;
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

                    Trace.WriteLine(moveDir.ToString());

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
}
