using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {        
        private void menuOpenProject_Click(object sender, RoutedEventArgs e)
        {
            if (!StackIsLoaded)
            {
                StatusText = "Projekt wird geöffnet";
                ProjectText = "";

                if (openProjectFile())
                    loadLabels();
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
                    Project.createZipFromWorkspace(Workspace.TempFolder, @"C:\APHA\Projects\");
                    StatusText = "Projekt wird geöffnet";
                    
                    if (openProjectFile())
                        loadLabels();
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    StatusText = "Projekt wird geöffnet";
                    ProjectText = "";
                    
                    if (openProjectFile())
                        loadLabels();
                }
            }          
        }

        private bool openProjectFile()
        {
            OpenFileDialog newOpenFileDialog = new OpenFileDialog();
            newOpenFileDialog.InitialDirectory = rootAppFolder + @"\Projects";
            newOpenFileDialog.Filter = "zip files (*.zip)|*.zip";

            if (newOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    refreshSession();
                    Workspace = new Workspace(System.IO.Path.GetFileNameWithoutExtension(newOpenFileDialog.SafeFileName));
                    Workspace.createFromZip(newOpenFileDialog.FileName);
                    Project = HausarbeitAPProjectCT.createFromFile(Path.Combine(Workspace.TempFolder, "project.xml"));
                    Project.initFileListFromStack(Workspace.TempFolder);
                    stackSlider.Minimum = 0;
                    stackSlider.Maximum = Project.totalLayers - 1;
                    loadPicture(0);
                    StackIsLoaded = true;
                    SectionView = false;
                    ProjectText = Project.description;
                    this.Title = System.IO.Path.GetFileNameWithoutExtension(Project.name);
                    return true;
                }
                catch (Exception exc)
                {
                    System.Windows.Forms.MessageBox.Show("Das Projekt konnte nicht geöffnet werden\n" + exc.Message + exc.InnerException, "Achtung");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
