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
using System.Threading;

namespace AP_HA
{
    public partial class MainWindow
    {        
        private void menuOpenProject_Click(object sender, RoutedEventArgs e)
        {
            StatusText = "Projekt wird geöffnet";
            OpenFileDialog newOpenFileDialog = new OpenFileDialog();
            newOpenFileDialog.InitialDirectory = @"C:\APHA\Projects\";
            newOpenFileDialog.Filter = "zip files (*.zip)|*.zip";

            if (newOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openProject(System.IO.Path.GetFileNameWithoutExtension(newOpenFileDialog.SafeFileName), newOpenFileDialog.FileName);
            }           
        }

        public void openProject(string projectName, string sourcePath)
        {           
            try
            {               
                Workspace = new Workspace(projectName);
                Workspace.createFromZip(sourcePath);
                Project = new HausarbeitAPProjectCT(sourcePath);
                Project.initFileListFromStack(Workspace.TempFolder);
                loadPicture(0);
                stackSlider.Maximum = Project.totalLayers - 1;
                StackIsLoaded = true;
                this.Title = System.IO.Path.GetFileNameWithoutExtension(Project.ProjectName);
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Das Projekt konnte nicht geöffnet werden\n" + exc.Message);
            }
        }
    }
}
