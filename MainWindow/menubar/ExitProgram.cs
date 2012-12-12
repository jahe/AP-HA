using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void exitProgram(object sender, RoutedEventArgs e)        //Menü->Datei->Beenden
        {
            if (StackIsLoaded)
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Wollen sie die Anwendung beenden?\nAlle nicht gespeicherte Projekte gehen verloren!",
                                  "Achtung",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (Directory.Exists(workspaceFolder))
                    {
                        DataProcessor.deleteAllSubfolders(workspaceFolder);
                    }

                    System.Windows.Application.Current.Shutdown();
                }
            }
            else
            {
                if (Directory.Exists(workspaceFolder))
                {
                    DataProcessor.deleteAllSubfolders(workspaceFolder);
                }

                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
