using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.IO.Packaging;
using System.Security.Permissions;

namespace AP_HA
{
    public class Project
    {
        private string folderPath;                                  //Aktueller Ordnerpfad
        private string folderName;
        private List<string> filePathList;                          //Liste der im Ordner enthaltenen *.tif
        private string[] filePaths;

        #region Properties
        public string FolderName
        {
            get { return this.folderName; }
            set { this.folderName = value; }
        }
        public string FolderPath
        {
            get { return this.folderPath; }
            private set { this.folderPath = value; }
        }
        public double Height
        {
            get;
            private set;
        }
        public double Width
        {
            get;
            private set;
        }
        public int PictureAmount
        {
            get { return this.filePathList.Count(); }
        }
     
        private string _projectName;
        public string ProjectName
        {
            get { return this._projectName; }
            private set { this._projectName = value; }
        }
        #endregion

        #region Constructors
        public Project()
        {

        }
        
        public Project(string name)
        {
            ProjectName = name;
        }

        //public Project(String zipPath)
        //{
            //vorhandenes ProjektZip öffnen
        //}
        #endregion

        public void createProjectZip(string path)
        {
            DirectoryInfo d = System.IO.Directory.CreateDirectory(path);

            string fileName = System.IO.Path.Combine(d.FullName, ProjectName);
            
            Package.Open(fileName, FileMode.Create);
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
                    FolderPath = path;

                    try
                    {
                        FileStream imgStream = new FileStream(this.getPictureFromList(0), FileMode.Open, FileAccess.Read, FileShare.Read);
                        TiffBitmapDecoder decoder = new TiffBitmapDecoder(imgStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                        BitmapSource bmpSrc = decoder.Frames[0];

                        Height = bmpSrc.PixelHeight;
                        Width = bmpSrc.PixelWidth;
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
