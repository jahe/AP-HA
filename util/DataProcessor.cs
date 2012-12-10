using System;
using System.IO;
using System.IO.Packaging;
using System.Windows.Media.Imaging;

namespace AP_HA
{
    public static class DataProcessor
    {
        public static void deleteAllSubfolders(string directoryPath)
        {
            foreach (string directoryName in System.IO.Directory.GetDirectories(directoryPath))
            {
                try
                {
                    System.IO.Directory.Delete(directoryName, true);
                }
                catch(Exception exc)
                {
                    //MessageBox.Show("Datei konnte nicht gelöscht werden\n"+exc.Message);
                }
            }
        }

        public static void extractToDirectory(string sourcePath, string targetPath)
        {
            using (Package zip = Package.Open(sourcePath, FileMode.Open, FileAccess.Read))
            {
                Uri FileNameXML = PackUriHelper.CreatePartUri(new Uri(".\\project.xml", UriKind.Relative));
                PackagePart zippedFileXML = zip.GetPart(FileNameXML);
                Stream filestreamXML = zippedFileXML.GetStream();
                HausarbeitAPProjectCT deserializedXMLFile = HausarbeitAPProjectCT.createFromStream(zippedFileXML.GetStream());
                string temppathXML = Path.Combine(targetPath, "project.xml");
                streamToFile(filestreamXML, temppathXML);
                for (int i = 0; i < deserializedXMLFile.totalLayers; i++)
                {
                    Uri FileNameTIFF = PackUriHelper.CreatePartUri(new Uri(".\\" + i.ToString("D" + deserializedXMLFile.totalLayers.ToString("D").Length.ToString()) + ".tif", UriKind.Relative));
                    Uri FileNameBMP = PackUriHelper.CreatePartUri(new Uri(".\\" + i.ToString("D" + deserializedXMLFile.totalLayers.ToString("D").Length.ToString()) + ".bmp", UriKind.Relative));
                    PackagePart zippedFileTIFF = zip.GetPart(FileNameTIFF);
                    PackagePart zippedFileBMP = zip.GetPart(FileNameBMP);
                    Stream filestreamTIFF = zippedFileTIFF.GetStream();
                    Stream filestreamBMP = zippedFileBMP.GetStream();
                    string temppathTIFF = Path.Combine(targetPath, i.ToString("D" + deserializedXMLFile.totalLayers.ToString("D").Length.ToString()) + ".tif");
                    string temppathBMP = Path.Combine(targetPath, i.ToString("D" + deserializedXMLFile.totalLayers.ToString("D").Length.ToString()) + ".bmp");
                    streamToFile(filestreamTIFF, temppathTIFF);                   
                    streamToFile(filestreamBMP, temppathBMP);
                }
            }
        }

        private static void streamToFile(Stream inputStream, string outputFile)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream");
            }

            if (String.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentException("Argument null or empty.", "outputFile");
            }
                
            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                int cnt = 0;
                const int LEN = 4096;
                byte[] buffer = new byte[LEN];

                while ((cnt = inputStream.Read(buffer, 0, LEN)) != 0)
                {
                    outputStream.Write(buffer, 0, cnt);
                }                    
            }
        }

        public static BitmapSource getImgFromPath(string path)
        {
            BitmapSource img;
            Stream imgStreamSource = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            TiffBitmapDecoder decoder = new TiffBitmapDecoder(imgStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            img = decoder.Frames[0];

            return img;
        }
    }
}
