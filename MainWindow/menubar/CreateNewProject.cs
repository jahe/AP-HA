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
                    LoadingWindow lw = new LoadingWindow("Neuer Stapel wird vorbereitet");
                    lw.Show();
                    //Workspace in temp anlegen
                    Workspace = new Workspace(createProjectDialog.NewProjectName);
                    //Bilder in Workspace kopieren und umbennen
                    Workspace.createWorkspacefromStack(createProjectDialog.StackPath);

                    //Project-Objekt erstellen und mit Daten aus dem tempfolder füllen                    
                    Project = new HausarbeitAPProjectCT(createProjectDialog.NewProjectName);
                    Project.initFileListFromStack(Workspace.TempFolder);
                    //XML Datei erstellen
                    Project.SaveToFile(Workspace.TempFolder + @"\project.xml");
                    //newProject.createZipFromStack(createProjectDialog.StackPath, newWorkspace.Name);

                    loadPicture(0);
                    stackSlider.Maximum = Project.totalLayers - 1;

                    lw.Close();
                }               
                catch (ProjectException pe)
                {
                    System.Windows.MessageBox.Show("Das Projekt konnte nicht erstellt werden\n" + pe.Message);
                    //TO DO Programm refreshen
                }                
            }
        }
    }
}
