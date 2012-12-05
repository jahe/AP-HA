using System;
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
    public static class DataProcessor
    {
        public static void deleteAllSubfolders(string directoryPath)
        {
            foreach (string directoryName in System.IO.Directory.GetDirectories(directoryPath))
            {
                try
                {
                    System.IO.Directory.Delete(directoryName, true);
                }
                catch(Exception exc)
                {
                    
                }
            }
        }

        public static void extractToDirectory(string sourcePath, string targetPath)
        {
            HausarbeitAPProjectCT deserializedXMLFile;
            Uri FileName;
            PackagePart zippedFile;
            Stream filestream;
            string temppath;

            using (Package zip = Package.Open(sourcePath, FileMode.Open, FileAccess.Read))
            {
                FileName = PackUriHelper.CreatePartUri(new Uri(".\\" + "project.xml", UriKind.Relative));
                zippedFile = zip.GetPart(FileName);
                filestream = zippedFile.GetStream();
                deserializedXMLFile = HausarbeitAPProjectCT.createFromStream(zippedFile.GetStream());           
                temppath = Path.Combine(targetPath, "project.xml");
                streamToFile(filestream, temppath);

                for (int i = 0; i < deserializedXMLFile.totalLayers; i++)
                {
                    FileName = PackUriHelper.CreatePartUri(new Uri(".\\" + i.ToString("D" + deserializedXMLFile.totalLayers.ToString("D").Length.ToString()) + ".tif", UriKind.Relative));
                    zippedFile = zip.GetPart(FileName);
                    filestream = zippedFile.GetStream();
                    temppath = Path.Combine(targetPath, i.ToString("D" + deserializedXMLFile.totalLayers.ToString("D").Length.ToString()) + ".tif");
                    streamToFile(filestream, temppath);
                }
            }
        }

        private static void streamToFile(Stream inputStream, string outputFile)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream");
            }

            if (String.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentException("Argument null or empty.", "outputFile");
            }
                
            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                int cnt = 0;
                const int LEN = 4096;
                byte[] buffer = new byte[LEN];

                while ((cnt = inputStream.Read(buffer, 0, LEN)) != 0)
                {
                    outputStream.Write(buffer, 0, cnt);
                }                    
            }
        }
    }
}
