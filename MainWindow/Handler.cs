using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;

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
    }
}
