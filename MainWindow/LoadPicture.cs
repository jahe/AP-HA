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
                StatusText = System.IO.Path.GetFileName(Project.getPictureFromList(picNo)) +" | Bild: "+picNo.ToString()+"/" + (Project.totalLayers - 1) + 
                            " Gesamtbildern. | Aktuelle Ansicht von: " + (int)stackSlider.Minimum +" bis " + (int)stackSlider.Maximum +" |";
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler bei der Bild(de)codierung\n" + e.Message + "\nAktueller Stapel muss geschlossen werden");
                refreshSession();
            }
        }
    }
}
