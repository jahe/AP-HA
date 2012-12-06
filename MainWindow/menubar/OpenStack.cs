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
                    loadPicture(0);
                    stackSlider.Maximum = Project.totalLayers - 1;
                    stackSlider.Value = 0;
                    StackIsLoaded = true;
                    this.Title = System.IO.Path.GetFileNameWithoutExtension(Project.ProjectName);
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
