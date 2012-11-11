using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AP_HA
{
    /** Klasse zum Verwalten des Bilderstapels / Ordners,
        mögliche Funktionen:
            .isValid - überprüft ob der Ordnerinhalt den Anforderungen entspricht
            .count - gibt die Anzahl der Bilder im Ordner wieder
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
        public string path;
        public int pictureAmount;

        public PictureStack()
        {
            //TO DO throw new noFolderException("Kein Ordner ausgewählt");
        }

        public PictureStack(string path)
        {
            if (isValid(path))
            {
                this.path = path;
                count(this.path);
            }
            else
            {
                //TO DO throw new invalidFolderException("Ordner entspricht nicht den Anforderungen");
            }
        }

        private bool isValid(string path)
        {
            //TO DO Logik zur Überprüfung des Ordnerinhalts implementieren
            return true;
        }

        private int count(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            this.pictureAmount = di.GetFiles().Length;
            return di.GetFiles().Length;
        }
    }
}
