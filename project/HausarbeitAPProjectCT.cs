﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.IO.Packaging;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.Threading;

namespace AP_HA
{       
    public partial class HausarbeitAPProjectCT
    {
        private List<string> filePathList;
        private string[] filePaths;

        #region Properties
        [XmlIgnore()]
        public string ProjectName { get; set; }
        #endregion

        #region Constructors
        public HausarbeitAPProjectCT()
        {

        }

        public HausarbeitAPProjectCT(string name)
        {
            ProjectName = name;
        }

        #endregion

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

        public void createZipFromStack(string sourcePath, string targetPath)
        {
            initFileListFromStack(sourcePath);
            
            DirectoryInfo d = System.IO.Directory.CreateDirectory(targetPath);
            string projectZipPath = System.IO.Path.Combine(d.FullName, ProjectName + ".zip");

            using (Package package = Package.Open(projectZipPath, FileMode.Create))
            {
                Uri FileName = PackUriHelper.CreatePartUri(new Uri(".\\project.xml", UriKind.Relative));
                PackagePart part = package.CreatePart(FileName, String.Empty, CompressionOption.Maximum);
                this.SaveToStream(part.GetStream());

                for (int i = 0; i < filePaths.Length; i++)
                {
                    Uri partUriResource = PackUriHelper.CreatePartUri(new Uri((i.ToString("D" + totalLayers.ToString("D").Length.ToString()) + ".tif"), UriKind.Relative));
                    PackagePart packagePartResource = package.CreatePart(partUriResource, System.Net.Mime.MediaTypeNames.Image.Tiff);

                    using (FileStream fileStream = new FileStream(filePaths[i], FileMode.Open, FileAccess.Read))
                    {
                        CopyStream(fileStream, packagePartResource.GetStream());
                    }                                        
                }
            }
        }

        private static void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
            {
                target.Write(buf, 0, bytesRead);
            }
        }

        public void initFileListFromStack(string path)
        {
            if (Directory.Exists(path))
            {
                filePathList = new List<string>();
                filePaths = System.IO.Directory.GetFiles(path, "*.tif", SearchOption.TopDirectoryOnly);

                for (int i = 0; i < filePaths.Length; i++)
                {
                    filePathList.Add(filePaths[i].ToString());
                }

                filePathList.Sort((a, b) => new StringSorter(a).CompareTo(new StringSorter(b)));

                if (filePathList.Count() != 0)
                {
                    totalLayers = filePathList.Count();

                    try
                    {
                        FileStream imgStream = new FileStream(this.getPictureFromList(0), FileMode.Open, FileAccess.Read, FileShare.Read);
                        TiffBitmapDecoder decoder = new TiffBitmapDecoder(imgStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                        BitmapSource bmpSrc = decoder.Frames[0];

                        height = bmpSrc.PixelHeight;
                        width = bmpSrc.PixelWidth;
                    }
                    catch (Exception e)
                    {
                        throw new ProjectException("Fehler bei der Bildstapelverarbeitung\n" + e.Message);
                    }
                }
                else
                {
                    throw new ProjectException("Der gewählte Ordner enthält keine *.tif Dateien");
                }
            }
            else
            {
                throw new ProjectException("Der ausgewählte Ordner konnte nicht gefunden werden");
            }
        }

        /**public static object LoadFromFile(string fileName)
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
        }**/

        /**Wenn man die Project-Klasse um Properties erweitern möchte, aber
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
        }**/
    }
}
