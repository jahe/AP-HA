using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Documents;

namespace AP_HA
{
    class PictureStack
    {
        private string folderPath;                                  //Aktueller Ordnerpfad
        private List<string> filePathList;                          //Liste der im Ordner enthaltenen *.tif
        private string[] filePaths;

        #region Constructors
        public PictureStack()
        {

        }

        public PictureStack(string path)                            
        {
            initFileList(path);

            if (filePathList.Count() != 0)                          
            {                               
                this.folderPath = path;
            }
            else
            {
                throw new PictureStackException("Der gewählte Ordner enthält keine *.tif Dateien");
            }
        }
        #endregion

        public int PictureAmount
        {
            get { return this.filePathList.Count(); }
        }

        public string getPictureFromList(int picNo)
        {
            return this.filePathList[picNo];
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
