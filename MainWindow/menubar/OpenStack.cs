using System;
using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuOpenStack(object sender, RoutedEventArgs e)
        {
            StatusText = "Neuer Workspace wird erstellt";
            CreateProjectDialog createProjectDialog = new CreateProjectDialog();

            if (createProjectDialog.DialogResult.HasValue && createProjectDialog.DialogResult.Value)
            {
                try
                {                  
                    Workspace = new Workspace(createProjectDialog.NewProjectName);
                    Workspace.copyStackFolder(createProjectDialog.StackPath);
                    Project = new HausarbeitAPProjectCT(createProjectDialog.NewProjectName);
                    Project.initFileListFromStack(Workspace.TempFolder);
                    Project.SaveToFile(Workspace.TempFolder + @"\project.xml");
                    stackSlider.Maximum = Project.totalLayers - 1;
                    stackSlider.Value = 0;
                    loadPicture(0);
                    StackIsLoaded = true;
                    this.Title = System.IO.Path.GetFileNameWithoutExtension(Project.name);
                }               
                catch (Exception exc)
                {
                    System.Windows.MessageBox.Show("Das Projekt konnte nicht erstellt werden\n" + exc.Message + exc.StackTrace);
                    refreshSession();
                } 
            }
        }
    }
}
