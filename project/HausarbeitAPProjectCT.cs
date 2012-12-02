using System;
using System.IO;
using System.Xml.Serialization;

namespace AP_HA
{
    public partial class HausarbeitAPProjectCT
    {
        public static HausarbeitAPProjectCT CreateFromFile(string fileName)
        {
            using (Stream s = System.IO.File.OpenRead(fileName))
            {
                return CreateFromStream(s);
            }
        }
        public static HausarbeitAPProjectCT CreateFromStream(Stream stream)
        {
            XmlSerializer x = new XmlSerializer(typeof(HausarbeitAPProjectCT));
            return (HausarbeitAPProjectCT)x.Deserialize(stream);
        }

        public void SaveToFile(string fileName)
        {
            using (Stream s = System.IO.File.Create(fileName))
            {
                SaveToStream(s);
            }
        }
        public void SaveToStream(Stream stream)
        {
            XmlSerializer x = new XmlSerializer(typeof(HausarbeitAPProjectCT));
            x.Serialize(stream, this);
        }

        //Wenn man die Project-Klasse um Properties erweitern möchte, aber
        //diese nicht mit in die XML-Datei schreiben will, kann man ihnen
        //das XmlIgnoreAttribute voranstellen.
        [XmlIgnore()]
        public System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(this.width, this.height);
            }
            set
            {
                if ((value.Width == 0) || (value.Height == 0))
                    throw new ArgumentException();
                this.width = value.Width;
                this.height = value.Height;
            }
        }
    }
}
