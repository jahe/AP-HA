using System;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuOpenStack(object sender, RoutedEventArgs e)
        {
            if (!StackIsLoaded)
            {
                StatusText = "Workspace wird erstellt";
                ProjectText = "";
                openStack();               
            }
            else
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Es ist noch ein Projekt geöffnet!\nMöchten sie es speichern bevor sie fortfahren?",
                                      "Achtung",
                                       MessageBoxButtons.YesNoCancel,
                                       MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button2);

                if (result == System.Windows.Forms.DialogResult.Yes) //Wenn Projekt gespeichert werden soll
                {
                    StatusText = "Projekt wird gespeichert";
                    ProjectText = "";
                    Project.createZipFromWorkspace(Workspace.TempFolder, rootAppFolder + @"\Projects");
                    openStack();
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    refreshSession();
                    openStack();
                }
            }

            updateLabels();
        }

        private void openStack()
        {
            refreshSession();
            StatusText = "Neuer Workspace wird erstellt";
            CreateProjectDialog createProjectDialog = new CreateProjectDialog();

            if (createProjectDialog.DialogResult.HasValue && createProjectDialog.DialogResult.Value)
            {
                try
                {
                    Workspace = new Workspace(createProjectDialog.NewProjectName);
                    Workspace.createFromPictureStack(createProjectDialog.StackPath);
                    Project = new HausarbeitAPProjectCT(createProjectDialog.NewProjectName);
                    Project.description = createProjectDialog.NewProjectDescription;
                    Project.initFileListFromStack(Workspace.TempFolder);
                    Project.SaveToFile(Workspace.TempFolder + @"\project.xml");
                    ProjectText = Project.description;
                    stackSlider.Maximum = Project.totalLayers - 1;
                    stackSlider.Value = 0;
                    loadPicture(0);
                    StackIsLoaded = true;
                    SectionView = false;
                    this.Title = System.IO.Path.GetFileNameWithoutExtension(Project.name);
                }
                catch (Exception exc)
                {
                    System.Windows.MessageBox.Show("Das Projekt konnte nicht erstellt werden\n" + exc.Message + exc.StackTrace);
                    refreshSession();
                }
            }
            else
            {
                refreshSession();
            }
        }
    }
}

