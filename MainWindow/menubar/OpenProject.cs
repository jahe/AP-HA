using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuOpenProject_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog newOpenFileDialog = new OpenFileDialog();
            newOpenFileDialog.InitialDirectory = "c:\\";
            newOpenFileDialog.Filter = "zip files (*.zip)|*.zip";

            if (newOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadingWindow lw = new LoadingWindow("Workspace aus Project-Datei wird erstellt");
                //Workspace in temp anlegen
                Workspace = new Workspace(System.IO.Path.GetFileNameWithoutExtension(newOpenFileDialog.SafeFileName));
                //Workspace mit Dateien aus dem Zip füllen
                Workspace.createWorkspacefromZip(newOpenFileDialog.FileName);
                
                Project = new HausarbeitAPProjectCT(newOpenFileDialog.FileName);
                Project.initFileListFromStack(Workspace.TempFolder);
                loadPicture(0);
                stackSlider.Maximum = Project.totalLayers - 1;
            }           
        }
    }
}
