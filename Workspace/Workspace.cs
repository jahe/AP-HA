using System.Linq;
using System.IO;
using System.IO.Compression;
using System;
using System.Collections.Generic;


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
            DataProcessor.deleteAllSubfolders(TargetFolder);
        }
        public Workspace(string name)
        {
            DataProcessor.deleteAllSubfolders(TargetFolder);
            rdNo = rd.Next(1, 1000);
            Name = name;
            TempFolder = TargetFolder + Name + rdNo.ToString();
        }
        #endregion

        #region Properties
        public string Name { get; private set; }
        
        public string TempFolder { get; private set; }
        #endregion

        public void createFromZip(string zipPath)
        {
            Directory.CreateDirectory(TempFolder);
            DataProcessor.extractToDirectory(zipPath, TempFolder);           
        }
        
        public void copyStackFolder(string sourceFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceFolder);
          
            if (!dir.Exists)
            {                
                throw new DirectoryNotFoundException("Der Quellpfad wurde nicht gefunden:\n" + sourceFolder);
            }

            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            FileInfo[] files = dir.GetFiles("*.tif", SearchOption.TopDirectoryOnly);
            List<FileInfo> filePathList = new List<FileInfo>();

            for (int i = 0; i < files.Length; i++)
            {
                filePathList.Add(files[i]);
            }

            filePathList.Sort((a, b) => new StringSorter(a.ToString()).CompareTo(new StringSorter(b.ToString())));
            
            int n = 0;
            foreach (FileInfo file in filePathList)
            {
                string temppath = Path.Combine(TempFolder, n.ToString("D" + files.Count().ToString("D").Length.ToString()) + ".tif");
                file.CopyTo(temppath, false);

                temppath = Path.Combine(TempFolder, n.ToString("D" + files.Count().ToString("D").Length.ToString()) + ".bmp");
                file.CopyTo(temppath, false);
                n++;
            }
        }
    }
}
