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
                if (SectionView)
                {
                    BitmapSource stackImage = DataProcessor.getImgFromPath(Project.getPictureFromList(picNo));
                    Int32Rect rect = new Int32Rect(Project.section.x, Project.section.y, Project.section.width, Project.section.height);
                    CroppedBitmap cb = new CroppedBitmap(stackImage, rect);
                    canvas.Width = cb.PixelWidth;
                    canvas.Height = cb.PixelHeight;
                    imgControl.Source = cb;
                    StatusText = System.IO.Path.GetFileName(Project.getPictureFromList(picNo)) + " | Bild: " + picNo.ToString() + "/" + (Project.totalLayers - 1) +
                                " Gesamtbildern. | Aktuelle Ansicht von: " + (int)stackSlider.Minimum + " bis " + (int)stackSlider.Maximum + " |";
                }
                else if (!SectionView)
                {
                    BitmapSource stackImage = DataProcessor.getImgFromPath(Project.getPictureFromList(picNo));
                    canvas.Width = stackImage.PixelWidth;
                    canvas.Height = stackImage.PixelHeight;
                    imgControl.Source = stackImage;
                    StatusText = System.IO.Path.GetFileName(Project.getPictureFromList(picNo)) + " | Bild: " + picNo.ToString() + "/" + (Project.totalLayers - 1) +
                                " Gesamtbildern. | Aktuelle Ansicht von: " + (int)stackSlider.Minimum + " bis " + (int)stackSlider.Maximum + " |";
                }

                alignMarksBySection();
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler bei der Bild(de)codierung\n" + e.Message + "\nAktueller Stapel muss geschlossen werden");
                refreshSession();
            }
        }
    }
}
