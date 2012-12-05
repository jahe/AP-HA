using System.Linq;
using System.IO;
using System.IO.Compression;
using System;


namespace AP_HA
{
    public class Workspace
    {
        public string TargetFolder = @"C:\APHA\workspace\";
        private Random rd = new Random();
        private int rdNo;

        #region Constructors
        public Workspace()
        {
            //DataProcessor.deleteAllSubfolders(@"C:\APHA\Workspace");
        }
        public Workspace(string name)
        {
            rdNo = rd.Next(1, 1000);
            Name = name;
            TempFolder = TargetFolder + Name + rdNo.ToString();
        }
        #endregion

        #region Properties
        public string Name { get; private set; }
        
        public string TempFolder { get; private set; }
        #endregion

        public void createFromStack (string stackFolderPath)
        {
            copyStackFolder(stackFolderPath);
        }

        public void createFromZip(string zipPath)
        {
            Directory.CreateDirectory(TempFolder);
            DataProcessor.extractToDirectory(zipPath, TempFolder);           
        }
        
        public void copyStackFolder(string sourceFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceFolder);
            DirectoryInfo[] d = dir.GetDirectories();
          
            if (!dir.Exists)
            {
                //throw new DirectoryNotFoundException("Der Quellpfad wurde nicht gefunden:\n" + sourceFolder);
            }

            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            FileInfo[] files = dir.GetFiles("*.tif", SearchOption.TopDirectoryOnly);
            int i = 0;
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(TempFolder, i.ToString("D" + files.Count().ToString("D").Length.ToString()) + ".tif");
                file.CopyTo(temppath, false);
                i++;
            }
        }
    }
}
