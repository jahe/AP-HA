using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSaveProject_Click(object sender, RoutedEventArgs e)
        {
            StatusText = "Aktuelles Projekt wird gespeichert";

            savePicture((int)stackSlider.Value);

            if(Project.createZipFromWorkspace(Workspace.TempFolder, @"C:\APHA\Projects\"))
            {
                refreshSession();
            }
        }
    }
}
