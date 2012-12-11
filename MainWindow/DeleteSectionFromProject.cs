using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void deleteSectionFromProject(object sender, RoutedEventArgs e)
        {
            if (Project.section != null)
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Möchten sie die aktuelle Section im Projekt wirklich löschen?",
                                  "Achtung",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes) //Wenn Section gelöscht werden soll
                {
                    showOriginalStack();
                    Project.section = null;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Löschen nicht möglich\nEs konnte keine Section gefunden werden");
            }
        }
    }
}
