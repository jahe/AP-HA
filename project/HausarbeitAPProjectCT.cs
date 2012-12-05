using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO.Packaging;
using System.Windows.Forms;

namespace AP_HA
{       
    public partial class HausarbeitAPProjectCT
    {
        private List<string> filePathList;
        private string[] filePaths;
        private LoadingWindow lw;

        #region Constructors
        public HausarbeitAPProjectCT()
        {

        }

        public HausarbeitAPProjectCT(string name)
        {
            ProjectName = name;
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

        
        public string ProjectName { get; set; }

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

        public void createZipFromWorkspace(string sourcePath, string targetPath)
        {
            Uri FileName;
            PackagePart part;
            
            DirectoryInfo d = System.IO.Directory.CreateDirectory(targetPath);            
            string projectZipPath = System.IO.Path.Combine(d.FullName, ProjectName);

            if (File.Exists(projectZipPath)) //Wenn die Zieldatei bereits besteht, Fragen ob Y/N/C  IN BEARBEITUNG
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Es ist bereits eine Datei für dieses Projekt im Zielordner vorhanden\nMöchten sie die Datei überschreiben?",
                                  "Achtung",
                                   MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes)    //Wenn überschrieben werden darf
                {
                    using (Package package = Package.Open(projectZipPath, FileMode.Create))
                    {
                        FileName = PackUriHelper.CreatePartUri(new Uri(".\\project.xml", UriKind.Relative));
                        part = package.CreatePart(FileName, String.Empty, CompressionOption.Maximum);
                        this.SaveToStream(part.GetStream());

                        for (int i = 0; i < filePaths.Length; i++)
                        {
                            FileName = PackUriHelper.CreatePartUri(new Uri((i.ToString("D" + totalLayers.ToString("D").Length.ToString()) + ".tif"), UriKind.Relative));
                            part = package.CreatePart(FileName, System.Net.Mime.MediaTypeNames.Image.Tiff);

                            using (FileStream fileStream = new FileStream(filePaths[i], FileMode.Open, FileAccess.Read))
                            {
                                copyStream(fileStream, part.GetStream());
                            }
                        }
                    }
                    lw = new LoadingWindow("Projekt wird gespeichert");
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    SaveFileDialog sFD = new SaveFileDialog();
                    sFD.InitialDirectory = d.FullName;
                    sFD.FileName = System.IO.Path.GetFileNameWithoutExtension(projectZipPath);
                    sFD.Filter = "zip files (*.zip)|*.zip";

                    if (sFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        using (Package package = Package.Open(sFD.FileName, FileMode.Create))
                        {
                            FileName = PackUriHelper.CreatePartUri(new Uri(".\\project.xml", UriKind.Relative));
                            part = package.CreatePart(FileName, String.Empty, CompressionOption.Maximum);
                            this.SaveToStream(part.GetStream());

                            for (int i = 0; i < filePaths.Length; i++)
                            {
                                FileName = PackUriHelper.CreatePartUri(new Uri((i.ToString("D" + totalLayers.ToString("D").Length.ToString()) + ".tif"), UriKind.Relative));
                                part = package.CreatePart(FileName, System.Net.Mime.MediaTypeNames.Image.Tiff);

                                using (FileStream fileStream = new FileStream(filePaths[i], FileMode.Open, FileAccess.Read))
                                {
                                    copyStream(fileStream, part.GetStream());
                                }
                            }
                        }
                        lw = new LoadingWindow("Projekt wird gespeichert");
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

                        ImgHeight = bmpSrc.PixelHeight;
                        ImgWidth = bmpSrc.PixelWidth;

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

        public string getPictureFromList(int picNo)
        {
            if (picNo < filePathList.Count() && picNo >= 0)
            {
                return this.filePathList[picNo];
            }
            else
            {
                throw new ProjectException("Gewähltes Bild nicht im Stapel vorhanden\nNegativ oder größer als Stapel");
            }
        }
    }
}
