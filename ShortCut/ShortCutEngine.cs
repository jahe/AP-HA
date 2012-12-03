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
        public List<ShortCut> ShortCuts
        {
            get;
            set;
        }

        public ShortCutEngine()
        {
            ShortCuts = new List<ShortCut>();
        }

        public void onShortCutChanged(object sender, ShortCutEventArgs e)
        {
            foreach (ShortCut sc in ShortCuts)
            {
                if (sc.matches(e.Shortcut))
                    sc.executeFunction();
            }
        }

        public void addShortCut(ShortCut sc)
        {
            ShortCuts.Add(sc);
        }

        public void removeShortCut(ShortCut sc)
        {
            ShortCuts.Remove(sc);
        }

        public void setEvent(String func, Action e)
        {
            foreach (ShortCut sc in ShortCuts)
            {
                if (sc.Name == func)
                    sc.Execute += e;
            }
        }

        public ShortCut getShortcutFromName(String funcName)
        {
            foreach (ShortCut sc in ShortCuts)
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
            ShortCutEngine sce;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filepath, FileMode.Open);

            sce = (ShortCutEngine) formatter.Deserialize(stream);
            stream.Close();

            return sce;
        }
    }
}
