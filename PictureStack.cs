using System;
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
            Throw clauses einfügen
            Funktionen einfügen
            ...
    **/
   
    class PictureStack //UNDER CONSTRUCTION
    {
        private string path;                                    //Aktueller Pfad
        private List<string> FileList;                          //Liste der im Ordner enthaltenen *.tif

        public PictureStack()                                   
        {
            //TO DO throw new noFolderException("Kein Ordner ausgewählt");
        }

        public PictureStack(string path)                        //Gültiger Konstruktor
        {
            if (list(path).Count() != 0)                        //Überprüfen ob *.tif im Ordner vorhanden, gleichzeitiges Füllen der FileList
            {                               
                this.path = path;
            }
            else
            {
                //TO DO throw new invalidFolderException("Der gewählte Ordner enthält keine *.tif Dateien");
                System.Windows.Forms.MessageBox.Show("Der gewählte Ordner enthält keine *.tif Dateien");
            }
        }

        public string getPath()
        {
            return this.path;
        }

        public int getPictureAmount()
        {
            return this.FileList.Count();
        }

        public List<string> getPictureList()
        {
            return this.FileList;
        }

        public string getPictureFromList(int picNo)
        {
            return this.FileList[picNo];
        }

        private List<string> list(string path)
        {
            FileList = new List<string>();

            string[] Files = System.IO.Directory.GetFiles(path, "*.tif", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < Files.Length; i++)
            {
                FileList.Add(Files[i].ToString());
                //System.Windows.Forms.MessageBox.Show(Files[i].ToString());
            }

            return FileList;
        }
    }
}
