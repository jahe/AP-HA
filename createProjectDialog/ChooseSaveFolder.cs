using System.Windows;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class CreateProjectDialog
    {
        private void chooseSaveFolder(object sender, RoutedEventArgs e) //CreateProjectDialog -> Zielordner auswählen
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.Description = "Speicherort auswählen";
            openFolderDialog.ShowNewFolderButton = true;
            openFolderDialog.SelectedPath = @"C:\APHA\";

            DialogResult oFDResult = openFolderDialog.ShowDialog();

            if (oFDResult == System.Windows.Forms.DialogResult.OK)
            {
                SaveProjectPath = openFolderDialog.SelectedPath;
            }
            else
            {
                //Es wurde kein Ordner ausgewählt
            }
        }
    }
}
