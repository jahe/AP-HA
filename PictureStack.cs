using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AP_HA
{
    /** Klasse zum Verwalten des Bilderstapels,
        mögliche Funktionen:
        .isValid - überprüft ob der Ordnerinhalt den Anforderungen entspricht
        .count - gibt die Anzahl der Bilder im Ordner wieder
        .nextImage - zeigt das nächste Bild des Stapels an (evtl für stackSlider)
        .previousImage - s.o.
        ....
        ***/
    
    /** TO DO 
        Throw clauses einfügen
        Funktionen einfügen
        ...
        **/
   
    class PictureStack
    {
        private string path;

        public PictureStack()
        {
        }

        public PictureStack(string path)
        {
            this.path = path;
        }

        private static int count(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            return di.GetFiles().Length;
        }
    }
}
