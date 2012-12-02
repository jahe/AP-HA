using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.IO.Packaging;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace AP_HA
{
    public partial class HausarbeitAPProjectCT
    {
        private List<string> filePathList;                          //Liste der im Ordner enthaltenen *.tif
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

        public void loadStackInZip(string sourcePath, string targetPath)
        {
            initFileListFromStack(sourcePath);

            DirectoryInfo d = System.IO.Directory.CreateDirectory(targetPath);
            string projectZipPath = System.IO.Path.Combine(d.FullName, ProjectName + ".zip");

            using (Package package = Package.Open(projectZipPath, FileMode.Create))
            {
                for (int i = 0; i < filePaths.Length; i++)
                {
                    Uri partUriResource = PackUriHelper.CreatePartUri(new Uri((i.ToString("D" + totalLayers.ToString("D").Length.ToString())+".tif"), UriKind.Relative));

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

        private void initFileListFromStack(string path)
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
