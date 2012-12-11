using System.Windows;
using System.Windows.Forms;
using System;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void cutAfterCursor(object sender, RoutedEventArgs e)     //Menü->Bearbeiten->Stapel beschneiden->Bilder nach cursor
        {
            try
            {
                if (Project.section == null)
                {
                    Project.section = new HausarbeitAPSectionCT();
                    cutImgsAfter();
                }

                if (Project.section.depth > 0)
                {
                    DialogResult result = System.Windows.Forms.MessageBox.Show("Es sind bereits Section informationen hinterlegt\nMöchten sie die Section überschreiben?",
                                  "Achtung",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        cutImgsAfter();
                    }
                }
                else
                {
                    cutImgsAfter();
                }
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message + exc.InnerException);
            }
        }

        private void cutImgsAfter()
        {
            Project.section.z = (int)stackSlider.Minimum;
            stackSlider.Maximum = stackSlider.Value;
            Project.section.depth = (int)stackSlider.Maximum - Project.section.z;
            loadPicture((int)stackSlider.Value);
            StackIsCutted = true;
        }
    }
}
