using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSaveProject_Click(object sender, RoutedEventArgs e)
        {
            StatusText = "Aktuelles Projekt wird gespeichert";
            Project.createZipFromWorkspace(Workspace.TempFolder, @"C:\APHA\Projects\");
            refreshSession();
        }
    }
}
