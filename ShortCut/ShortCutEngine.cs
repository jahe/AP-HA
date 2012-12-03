using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AP_HA
{
    [Serializable()]
    public class ShortCutEngine
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

        public void setEvent(String func, Action e)
        {
            foreach (ShortCut sc in shortCuts)
            {
                if (sc.Name == func)
                    sc.Execute += e;
            }
        }

        public ShortCut getShortcutFromName(String funcName)
        {
            foreach (ShortCut sc in shortCuts)
            {
                if (sc.Name == funcName)
                    return sc;
            }

            return null;
        }

        public void Serialize(String filepath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filepath, FileMode.Create);

            formatter.Serialize(stream, this);
            stream.Close();
        }

        public static ShortCutEngine Deserialize(String filepath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filepath, FileMode.Open);

            return (ShortCutEngine) formatter.Deserialize(stream);
        }
    }
}
