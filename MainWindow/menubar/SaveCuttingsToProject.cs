using System.Windows;
using System;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSaveCuttingsToProject_Click(object sender, RoutedEventArgs e)
        {
            if (Project.section != null || Section != null)
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Es wurde bereits eine Section im aktuellen Projekt erstellt\nMöchten sie die Section ersetzen?",
                                  "Achtung",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    saveToSection();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Section wurde nicht gespeichert", "Achtung");
                }
            }
            else
            {
                saveToSection();
            }
        }

        private void saveToSection()
        {
            try
            {
                Section = new HausarbeitAPSectionCT();
                Section.z = (int)stackSlider.Minimum;
                Section.depth = (int)stackSlider.Maximum;
                Project.section = Section;
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Die Section konnte nicht erstellt werden\n" + exc.Message, "Achtung");
            }
        }
    }
}
