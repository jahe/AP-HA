﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Packaging;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AP_HA
{
    public class DataProcessor
    {
        public static void createFromDirectory(string sourcePath, string targetPath)
        {
            //TO DO Zipper implementieren
        }

        public static void deleteAllSubfolders(string directoryPath)
        {
            foreach (string directoryName in System.IO.Directory.GetDirectories(directoryPath))
            {
                try
                {
                    System.IO.Directory.Delete(directoryName, true);
                }
                catch
                {
                    //throw Exception ex; 
                }
            }
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


        public static void extractToDirectory(string sourcePath, string targetPath)
        {
            HausarbeitAPProjectCT deserializedXMLFile;
            Uri FileName;
            PackagePart zippedFile;
            Stream filestream;
            string temppath;

            using (Package zip = Package.Open(sourcePath, FileMode.Open, FileAccess.Read)) //Zip-Datei zum lesen öffnen
            {
                FileName = PackUriHelper.CreatePartUri(new Uri(".\\" + "project.xml", UriKind.Relative)); // Pfad innerhalb des zip files ist eine Url
                zippedFile = zip.GetPart(FileName); // Referenz auf eine Datei in dem zip file
                filestream = zippedFile.GetStream();
                deserializedXMLFile = HausarbeitAPProjectCT.CreateFromStream(zippedFile.GetStream()); // GetStream() liefert einen Stream, den wir einfach (wie z.b. einen FileStream) auslesen können
                
                temppath = Path.Combine(targetPath, "project.xml");
                StreamToFile(filestream, temppath);

                for (int i = 0; i < deserializedXMLFile.totalLayers; i++) //Für die Bilddaten machen wir praktisch das gleiche mit fortlaufenden Nummern.
                {
                    FileName = PackUriHelper.CreatePartUri(new Uri(".\\" + i.ToString("D" + deserializedXMLFile.totalLayers.ToString("D").Length.ToString()) + ".tif", UriKind.Relative));
                    zippedFile = zip.GetPart(FileName);
                    filestream = zippedFile.GetStream();

                    temppath = Path.Combine(targetPath, i.ToString("D" + deserializedXMLFile.totalLayers.ToString("D").Length.ToString()) + ".tif");

                    StreamToFile(filestream, temppath);
                }
            }
        }

        private static void StreamToFile(Stream inputStream, string outputFile)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (String.IsNullOrEmpty(outputFile))
                throw new ArgumentException("Argument null or empty.", "outputFile");

            using (FileStream outputStream = new FileStream(outputFile, FileMode.CreateNew, FileAccess.Write))
            {
                int cnt = 0;
                const int LEN = 4096;
                byte[] buffer = new byte[LEN];

                while ((cnt = inputStream.Read(buffer, 0, LEN)) != 0)
                    outputStream.Write(buffer, 0, cnt);
            }
        }
    }
}
