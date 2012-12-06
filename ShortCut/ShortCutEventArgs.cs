using System;

namespace AP_HA
{
    public class ShortCutEventArgs : EventArgs
    {
        public ShortCut Shortcut { get; set; }

        public ShortCutEventArgs()
        {

        }

        public ShortCutEventArgs(ShortCut sc)
        {
            Shortcut = sc;
        }
    }
}
