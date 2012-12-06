using System;
using System.Windows;
using System.Windows.Forms;

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
