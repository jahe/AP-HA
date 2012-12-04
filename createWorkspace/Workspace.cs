using System.Linq;
using System.IO;
using System.IO.Compression;


namespace AP_HA
{
    public class Workspace
    {
        #region Properties
        public string Name { get; private set; }
        private string TargetFolder = @"C:\APHA\temp\";
        public string TempFolder { get; private set; }
        #endregion
       
        #region Constructors
        public Workspace()
        {
            
        }
        public Workspace(string name)
        {
            Name = name;
            TempFolder = TargetFolder + Name;
        }
        #endregion

        public void createWorkspacefromStack (string stackFolderPath)
        {
            copyStackFolder(stackFolderPath);
        }

        public void createWorkspacefromZip(string zipPath)
        {
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
   
            /**if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }**/
        }
    }
}
