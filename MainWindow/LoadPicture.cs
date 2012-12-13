using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
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

                if (SectionView)
                {          
                    Int32Rect rect = new Int32Rect(Project.section.x, Project.section.y, Project.section.width, Project.section.height);
                    CroppedBitmap cb = new CroppedBitmap(stackImage, rect);
                    canvas.Width = cb.PixelWidth;
                    canvas.Height = cb.PixelHeight;
                    markCanvas.Width = Project.width;
                    markCanvas.Width = Project.height;
                    imgControl.Source = cb;
                }
                else if (!SectionView)
                {
                    canvas.Width = stackImage.PixelWidth;
                    canvas.Height = stackImage.PixelHeight;
                    markCanvas.Width = Project.width;
                    markCanvas.Width = Project.height;
                    imgControl.Source = stackImage;                    
                }

                StatusText = System.IO.Path.GetFileName(Project.getPictureFromList(picNo)) + " | Bild: " + picNo.ToString() + "/" + (Project.totalLayers - 1) +
                                " Gesamtbildern. | Aktuelle Ansicht von: " + (int)stackSlider.Minimum + " bis " + (int)stackSlider.Maximum + " |";

                loadSavedMarks(picNo);
                alignMarksBySection();
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler bei der Bild(de)codierung\n" + e.Message + "\nAktueller Stapel muss geschlossen werden");
                refreshSession();
            }
        }

        public void loadSavedMarks(int picNo)
        {
            string path = Path.Combine(Workspace.TempFolder, picNo.ToString("D" + Project.filePathListBMP.Count.ToString("D").Length) + @".bmp");
            //Uri imgUri = new Uri(path, UriKind.RelativeOrAbsolute);
            //savedMarks.Source = new BitmapImage(imgUri);

            BitmapSource bmpSource;
            Bitmap bmp;

            bmp = new Bitmap(path);
            bmp.MakeTransparent(System.Drawing.Color.Black);
            bmpSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero,Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            //BitmapSource bmpSource = new BitmapImage(new Uri(path));
            savedMarks.Source = bmpSource;
        }
    }
}
