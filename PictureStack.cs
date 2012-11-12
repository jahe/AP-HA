﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AP_HA
{
    /** Klasse zum Verwalten des Bilderstapels / Ordners,
            ....
    **/
    
    /** TO DO 
            Funktionen einfügen um nur bestimmten Bereich des Stapels anzuzeigen
    **/
   
    class PictureStack //UNDER CONSTRUCTION
    {
        private string folderPath;                                  //Aktueller Pfad
        private List<string> filePathList;                          //Liste der im Ordner enthaltenen *.tif

        public PictureStack()
        {
        
        }

        public PictureStack(string path)                            //Gültiger Konstruktor
        {
            initFileList(path);

            if (filePathList.Count() != 0)                          //Überprüfen ob *.tif im Ordner vorhanden, gleichzeitiges Füllen der FileList
            {                               
                this.folderPath = path;
            }
            else
            {
                throw new pictureStackException("Der gewählte Ordner enthält keine *.tif Dateien");
            }
        }

        public string getPath()
        {
            return this.folderPath;
        }

        public int getPictureAmount()
        {
            return this.filePathList.Count();
        }

        public List<string> getPictureList()
        {
            return this.filePathList;
        }

        public string getPictureFromList(int picNo)
        {
            return this.filePathList[picNo];
        }

        private void initFileList(string path)
        {
            filePathList = new List<string>();

            string[] filePaths = System.IO.Directory.GetFiles(path, "*.tif", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < filePaths.Length; i++)
            {
                filePathList.Add(filePaths[i].ToString());
            }
        }
    }
}
