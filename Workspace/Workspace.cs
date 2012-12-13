using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace AP_HA
{
    public class Workspace
    {       
        private static String rootAppFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public string TargetFolder = SpecialDirectories.MyDocuments + @"\JBPMBodyViewer\Workspace\";
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
        
        public void createFromPictureStack(string sourceFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceFolder);
            int n = 0;
          
            if (!dir.Exists)
            {                
                throw new DirectoryNotFoundException("Der Quellpfad wurde nicht gefunden:\n" + sourceFolder);
            }

            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            FileInfo[] tiffFiles = dir.GetFiles("*.tif", System.IO.SearchOption.TopDirectoryOnly);
            List<FileInfo> tiffFilePathList = new List<FileInfo>();

            for (int i = 0; i < tiffFiles.Length; i++)
            {
                tiffFilePathList.Add(tiffFiles[i]);
            }

            tiffFilePathList.Sort((a, b) => new StringSorter(a.ToString()).CompareTo(new StringSorter(b.ToString())));
                        
            foreach (FileInfo file in tiffFilePathList)
            {
                string temppathTIFF = Path.Combine(TempFolder, n.ToString("D" + tiffFiles.Count().ToString("D").Length.ToString()) + ".tif");
                file.CopyTo(temppathTIFF, false);

                System.Windows.Media.Imaging.BitmapSource tiffSource = DataProcessor.getImgFromPath(temppathTIFF);

                Bitmap blankBMP = new Bitmap(tiffSource.PixelWidth, tiffSource.PixelHeight);

                using (Graphics grp = Graphics.FromImage(blankBMP)) 
                {
                    SolidBrush brush = new SolidBrush(Color.Black);
                    grp.FillRectangle(brush, 0, 0, tiffSource.PixelWidth, tiffSource.PixelHeight);
                }

                string temppathBMP = Path.Combine(TempFolder, n.ToString("D" + tiffFiles.Count().ToString("D").Length.ToString()) + ".bmp");
                
                blankBMP.Save(temppathBMP);
                n++;
            }
        }
    }
}
