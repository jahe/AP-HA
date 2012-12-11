using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void cropBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Project.section == null)
                {
                    Project.section = new HausarbeitAPSectionCT();
                    cropIt();
                }
                else
                {
                    DialogResult result = System.Windows.Forms.MessageBox.Show("Es sind bereits Section informationen hinterlegt\nMöchten sie die Section überschreiben?",
                                  "Achtung",
                                   MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

                    if (result == System.Windows.Forms.DialogResult.Yes) //Wenn Zip überschrieben werden soll
                    {
                        cropIt();
                    }
                }               
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message + exc.InnerException);
            }
        }

        private void cropIt()
        {
            Project.section.x = (int)Canvas.GetLeft(cropRectangle);
            Project.section.y = (int)Canvas.GetTop(cropRectangle);
            Project.section.width = (int)cropRectangle.Width;
            Project.section.height = (int)cropRectangle.Height;
            cropRectangle.Visibility = Visibility.Collapsed;
            zoomExpander.IsEnabled = true;
            SectionView = true;
            loadPicture((int)stackSlider.Value);
        }
    }
}
