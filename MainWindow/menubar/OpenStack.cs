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

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuOpenStack(object sender, RoutedEventArgs e)
        {
            CreateProjectDialog createProjectDialog = new CreateProjectDialog();

            if (createProjectDialog.DialogResult.HasValue && createProjectDialog.DialogResult.Value)
            {
                try
                {
                    lw = new LoadingWindow("Neuer Workspace wird erstellt");                  
                    Workspace = new Workspace(createProjectDialog.NewProjectName);
                    Workspace.copyStackFolder(createProjectDialog.StackPath);
                    lw = new LoadingWindow("Bildstapeldaten werden geladen");                  
                    Project = new HausarbeitAPProjectCT(createProjectDialog.NewProjectName+".zip");
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
                    //refreshSession();
                } 
            }
        }
    }
}
