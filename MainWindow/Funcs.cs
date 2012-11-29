using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void zoomIn()
        {
            zoomSlider.Value += 0.1;
        }

        private void zoomOut()
        {
            zoomSlider.Value -= 0.1;
        }

        private void scrollIn()
        {
            if (!stackSlider.IsEnabled)
                return;

            stackSlider.Value += 1;
        }

        private void scrollOut()
        {
            if (!stackSlider.IsEnabled)
                return;

            stackSlider.Value -= 1;
        }
    }
}
