using System.Windows;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSaveProjectAs_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            DialogResult fBDResult = new DialogResult();
            fBD.ShowDialog();

            if (fBDResult == System.Windows.Forms.DialogResult.OK)
            {
                savePicture((int)stackSlider.Value);

                Project.createZipFromWorkspace(Workspace.TempFolder, fBD.SelectedPath);
            }
        }
    }
}
