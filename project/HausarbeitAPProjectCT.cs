using System;
using System.IO;
using System.Xml.Serialization;
using System.IO.Packaging;
using System.Windows.Controls;

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

        public static object LoadFromFile(string fileName)
        {
            HausarbeitAPProjectCT deserializedXMLFile;
            using (Package zip = Package.Open(fileName, FileMode.Open, FileAccess.Read)) //Zip-Datei zum lesen öffnen
            {
                Uri FileName = PackUriHelper.CreatePartUri(new Uri(".\\project.xml", UriKind.Relative)); // Pfad innerhalb des zip files ist eine Url
                PackagePart zippedFile = zip.GetPart(FileName); // Referenz auf eine Datei in dem zip file
                deserializedXMLFile = HausarbeitAPProjectCT.CreateFromStream(zippedFile.GetStream()); // GetStream() liefert einen Stream, den wir einfach (wie z.b. einen FileStream) auslesen können.
                for (int i = 0; i < deserializedXMLFile.totalLayers; i++) //Für die Bilddaten machen wir praktisch das gleiche mit fortlaufenden Nummern.
                {
                   // FileName = PackUriHelper.CreatePartUri(new Uri(".\\" + i.ToZeroLeadingString(deserializedXMLFile.totalLayers.ToString().Length) + ".tif", UriKind.Relative)); // Statt einer extension Int32.ToZeroLeadingString(int länge) kann man auch irgendeine andere Funktion implementieren die die führenden nullen auffüllt
                    //zippedFile = zip.GetPart(FileName);
                    //Image img = Image.FromStream(zippedFile.GetStream());
                }
            }
            return null;
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
