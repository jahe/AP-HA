using System;
using System.Windows;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void cutBeforeCursor(object sender, RoutedEventArgs e)    //Menü->Bearbeiten->Stapel beschneiden->Bilder vor cursor
        {
            try
            {
                if (Project.section == null)
                {
                    Project.section = new HausarbeitAPSectionCT();
                    cutImgsBefore();
                }
                else if (Project.section.z > 0)
                {
                    DialogResult result = System.Windows.Forms.MessageBox.Show("Es sind bereits Section Informationen für 'z' hinterlegt\nMöchten sie die Daten überschreiben?",
                                  "Achtung",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        cutImgsBefore();
                    }
                }
                else
                {
                    cutImgsBefore();
                }
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message + exc.InnerException);
            }
        }

        private void cutImgsBefore()
        {
            stackSlider.Minimum = stackSlider.Value;
            Project.section.z = (int)stackSlider.Minimum;
            loadPicture((int)stackSlider.Value);
            StackIsCutted = true;
        }
    }
}
