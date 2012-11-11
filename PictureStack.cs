using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AP_HA
{
    /** Klasse zum Verwalten des Bilderstapels / Ordners,
        mögliche Funktionen:
            .list - gibt eine Liste der vorhandenen Dateien wieder
            .nextImage - zeigt das nächste Bild des Stapels an (evtl für stackSlider)
            .previousImage - s.o.
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
        private int pictureAmount;                              //Anzahl der im Ordner befindlichen Bilder
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
                this.pictureAmount = FileList.Count();
            }
            else
            {
                //throw new invalidFolderException("Der gewählte Ordner enthält keine *.tif Dateien");
                System.Windows.Forms.MessageBox.Show("Der gewählte Ordner enthält keine *.tif Dateien");
            }
        }

        public string getPath()
        {
            return this.path;
        }

        public int getPictureAmount()
        {
            return this.pictureAmount;
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
