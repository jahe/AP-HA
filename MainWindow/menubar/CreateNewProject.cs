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
        HausarbeitAPProjectCT newProject;
        Workspace newWorkspace;
        private void menuCreateProject_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectDialog createProjectDialog = new CreateProjectDialog();

            if (createProjectDialog.DialogResult.HasValue && createProjectDialog.DialogResult.Value)
            {
                try
                {
                    DirectoryInfo d = System.IO.Directory.CreateDirectory(createProjectDialog.SaveProjectPath);
                    string projectZipPath = System.IO.Path.Combine(d.FullName, "project.xml");

                    //Workspace in temp anlegen
                    newWorkspace = new Workspace(createProjectDialog.NewProjectName);
                    //Bilder in Workspace kopieren und umbennen
                    newWorkspace.createWorkspacefromStack(createProjectDialog.StackPath);

                    //Project-Objekt erstellen und mit Daten aus dem tempfolder füllen                    
                    newProject = new HausarbeitAPProjectCT(createProjectDialog.NewProjectName);
                    newProject.initFileListFromStack(newWorkspace.TempFolder);
                    //XML Datei erstellen
                    newProject.SaveToFile(newWorkspace.TempFolder + @"\project.xml");
                    //newProject.createZipFromStack(createProjectDialog.StackPath, newWorkspace.Name);


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
