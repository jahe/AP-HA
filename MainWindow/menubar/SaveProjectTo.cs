using System.Windows;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSaveProjectAs_Click(object sender, RoutedEventArgs e)
        {
            saveToFolder();
        }

        private void saveToFolder()
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();

            if (fBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                savePicture((int)stackSlider.Value);
                Project.createZipFromWorkspace(Workspace.TempFolder, fBD.SelectedPath);
            }
        }
    }
}
