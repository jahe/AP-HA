using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AP_HA
{
    class ShortCutEngine
    {
        private List<ShortCut> shortCuts;

        public ShortCutEngine()
        {
            shortCuts = new List<ShortCut>();
        }

        public void onShortCutChanged(object sender, ShortCutEventArgs e)
        {
            foreach (ShortCut sc in shortCuts)
            {
                if (sc.matches(e.Shortcut))
                    sc.executeFunction();
            }
        }

        public void addShortCut(ShortCut sc)
        {
            shortCuts.Add(sc);
        }

        public void removeShortCut(ShortCut sc)
        {
            shortCuts.Remove(sc);
        }
    }
}
