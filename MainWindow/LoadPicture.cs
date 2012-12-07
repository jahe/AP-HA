using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AP_HA
{
    public partial class MainWindow
    {        
        public void loadPicture(int picNo)
        {
            try
            {
                BitmapSource stackImage = DataProcessor.getImgFromPath(Project.getPictureFromList(picNo));
                canvas.Width = stackImage.PixelWidth;
                canvas.Height = stackImage.PixelHeight;
                imgControl.Source = stackImage;
                StatusText = System.IO.Path.GetFileName(Project.getPictureFromList(picNo)) +" ("+picNo.ToString()+" von " + (Project.totalLayers - 1) + ")";
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler bei der Bild(de)codierung\n" + e.Message + "\nAktueller Stapel muss geschlossen werden");
                refreshSession();
            }
        }
    }
}
