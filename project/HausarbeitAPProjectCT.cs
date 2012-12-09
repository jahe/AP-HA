using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace AP_HA
{       
    public partial class HausarbeitAPProjectCT
    {
        private List<string> filePathListTIFF;
        private string[] filePathsTIFF;

        private List<string> filePathListBMP;
        private string[] filePathsBMP;

        Uri FileName;
        PackagePart part;

        #region Constructors
        public HausarbeitAPProjectCT()
        {

        }

        public HausarbeitAPProjectCT(string name)
        {
            this.name = Path.GetFileNameWithoutExtension(name);
        }

        #endregion

        #region Properties

        public event PropertyChangedEventHandler ChangedProperty;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = ChangedProperty;

            if (handler != null)
            {
                ChangedProperty(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [XmlIgnore()]
        //public string ProjectName { get; set; }

        private int _imgHeight;      
        [XmlIgnore()]
        public int ImgHeight 
        {
            get { return _imgHeight; }
            set
            {
                _imgHeight = value;
                height = value;
                OnPropertyChanged("ImgHeight");
            }
        }

        private int _imgWidth;
        [XmlIgnore()]
        public int ImgWidth 
        {
            get { return _imgWidth; }
            private set
            {
                width = value;
                _imgWidth = value;
                OnPropertyChanged("ImgWidth");
            }
        }
        #endregion

        public static HausarbeitAPProjectCT createFromFile(string fileName)
        {
            using (Stream s = System.IO.File.OpenRead(fileName))
            {
                return createFromStream(s);
            }
        }

        public static HausarbeitAPProjectCT createFromStream(Stream stream)
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

        public bool createZipFromWorkspace(string sourcePath, string targetPath)
        {            
            DirectoryInfo d = System.IO.Directory.CreateDirectory(targetPath);            
            string projectZipPath = System.IO.Path.Combine(d.FullName, name+".zip");

            if (File.Exists(projectZipPath)) //Wenn die Zieldatei bereits besteht
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Es ist bereits eine Datei für dieses Projekt im Zielordner vorhanden\nMöchten sie die Datei überschreiben?",
                                  "Achtung",
                                   MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes) //Wenn Zip überschrieben werden soll
                {
                    copyDataToZip(projectZipPath);
                    return true;
                }
                else if (result == System.Windows.Forms.DialogResult.No) //Wenn neuer Zielort gewählt werden soll
                {
                    SaveFileDialog sFD = new SaveFileDialog();
                    sFD.InitialDirectory = d.FullName;
                    sFD.FileName = System.IO.Path.GetFileNameWithoutExtension(projectZipPath);
                    sFD.Filter = "zip files (*.zip)|*.zip";

                    if (sFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        copyDataToZip(sFD.FileName);                        
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Das Projekt wurde nicht gespeichert", "Achtung");
                    return false;                   
                }
            }
            else //Wenn keine Datei mit dem Name existiert
            {                
                copyDataToZip(projectZipPath);
                return true;
            }   
        }

        private void copyDataToZip(string destinationPath)
        {
            using (Package package = Package.Open(destinationPath, FileMode.Create))
            {
                FileName = PackUriHelper.CreatePartUri(new Uri(".\\project.xml", UriKind.Relative));
                part = package.CreatePart(FileName, String.Empty, CompressionOption.Maximum);
                this.SaveToStream(part.GetStream());

                for (int i = 0; i < filePathsTIFF.Length; i++)
                {
                    FileName = PackUriHelper.CreatePartUri(new Uri((i.ToString("D" + totalLayers.ToString("D").Length.ToString()) + ".tif"), UriKind.Relative));
                    part = package.CreatePart(FileName, String.Empty, CompressionOption.Maximum);

                    using (FileStream fileStream = new FileStream(filePathsTIFF[i], FileMode.Open, FileAccess.Read))
                    {
                        copyStream(fileStream, part.GetStream());
                    }
                }

                for (int i = 0; i < filePathsBMP.Length; i++)
                {
                    FileName = PackUriHelper.CreatePartUri(new Uri((i.ToString("D" + totalLayers.ToString("D").Length.ToString()) + ".bmp"), UriKind.Relative));
                    part = package.CreatePart(FileName, String.Empty, CompressionOption.Maximum);

                    using (FileStream fileStream = new FileStream(filePathsBMP[i], FileMode.Open, FileAccess.Read))
                    {
                        copyStream(fileStream, part.GetStream());
                    }
                }
            }
        }

        private static void copyStream(Stream source, Stream target)
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
                filePathListTIFF = new List<string>();
                filePathsTIFF = System.IO.Directory.GetFiles(path, "*.tif", SearchOption.TopDirectoryOnly);

                for (int i = 0; i < filePathsTIFF.Length; i++)
                {
                    filePathListTIFF.Add(filePathsTIFF[i].ToString());
                }

                filePathListBMP = new List<string>();
                filePathsBMP = System.IO.Directory.GetFiles(path, "*.bmp", SearchOption.TopDirectoryOnly);

                for (int i = 0; i < filePathsBMP.Length; i++)
                {
                    filePathListBMP.Add(filePathsBMP[i].ToString());
                }

                if (filePathListBMP.Count == 0)
                {
                    throw new ProjectException("Die gewählte Projektdatei enthält keine Bitmaps");
                }

                if (filePathListBMP.Count != filePathListTIFF.Count)
                {
                    throw new ProjectException("Differenz von TIF und BMP-Dateien in Projektdatei");
                }

                if (filePathListTIFF.Count() != 0)
                {
                    totalLayers = filePathListTIFF.Count();

                    //try
                    //{
                    //    FileStream imgStream = new FileStream(this.getPictureFromList(0), FileMode.Open, FileAccess.Read, FileShare.Read);
                    //    TiffBitmapDecoder decoder = new TiffBitmapDecoder(imgStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    //    BitmapSource bmpSrc = decoder.Frames[0];

                    //    ImgHeight = bmpSrc.PixelHeight;
                    //    ImgWidth = bmpSrc.PixelWidth;

                    //}
                    //catch (Exception e)
                    //{
                    //    throw new ProjectException("Fehler bei der Bildstapelverarbeitung\n" + e.Message);
                    //}
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

        public string getPictureFromList(int picNo)
        {
            if (picNo < filePathListTIFF.Count() && picNo >= 0)
            {
                return this.filePathListTIFF[picNo];
            }
            else
            {
                throw new ProjectException("Gewähltes Bild nicht im Stapel vorhanden\nNegativ oder größer als Stapel");
            }
        }
    }
}
