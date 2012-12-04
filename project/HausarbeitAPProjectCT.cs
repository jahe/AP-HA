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

namespace AP_HA
{       
    public partial class HausarbeitAPProjectCT
    {
        private List<string> filePathList;
        private string[] filePaths;

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
                return "Fehler";
            }
        }
    }
}
