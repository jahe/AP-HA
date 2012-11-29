using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AP_HA
{
    class ShortCutEventArgs : EventArgs
    {
        public ShortCut Shortcut { get; set; }

        public ShortCutEventArgs(ShortCut sc)
        {
            Shortcut = sc;
        }
    }
}
