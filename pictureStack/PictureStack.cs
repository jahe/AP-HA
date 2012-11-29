using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace AP_HA
{
    public class PictureStack
    {
        private string folderPath;                                  //Aktueller Ordnerpfad
        private string folderName;
        private List<string> filePathList;                          //Liste der im Ordner enthaltenen *.tif
        private string[] filePaths;
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

        #region Constructors
        public PictureStack()
        {

        }

        public PictureStack(string path)                            
        {           
            initFileList(path);
        }
        #endregion

        public string getPictureFromList(int picNo)
        {
            if (picNo < filePathList.Count() && picNo >= 0)
            {
                return this.filePathList[picNo];
            }
            else
            {
                throw new PictureStackException("Zu ladende Bildnummer größer als Bildmenge oder negativ");
            }           
        }

        private void initFileList(string path)
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
                    FolderName = Path.GetFileName(path);

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
                        throw new PictureStackException("Fehler bei der Bildstapelverarbeitung\n" + e.Message);
                    }
                }
                else
                {
                    throw new PictureStackException("Der gewählte Ordner enthält keine *.tif Dateien");
                }                
            }
            else
            {
                throw new PictureStackException("Der ausgewählte Ordner konnte nicht gefunden werden");
            }            
        }

        public void stackReset()
        {
            filePathList = new List<string>();
            folderPath = null;
        }
    }
}
