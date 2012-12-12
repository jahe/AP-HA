using System.Windows;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class CreateProjectDialog
    {
        private void openStackFolder(object sender, RoutedEventArgs e)         //CreateProjectDialog->Stapel öffnen
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.Description = "Neuen Bildstapel auswählen";
            openFolderDialog.ShowNewFolderButton = false;
            openFolderDialog.SelectedPath = @"C:\";

            DialogResult oFDResult = openFolderDialog.ShowDialog();

            if (oFDResult == System.Windows.Forms.DialogResult.OK)
            {
                StackPath = openFolderDialog.SelectedPath;
                NewProjectName = System.IO.Path.GetFileName(StackPath);
            }
        }
    }
}
