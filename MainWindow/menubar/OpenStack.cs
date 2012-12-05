﻿using System;
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
                    Workspace = new Workspace(createProjectDialog.NewProjectName);
                    Workspace.createFromStack(createProjectDialog.StackPath);
                    lw = new LoadingWindow("Neuer Stapel wird vorbereitet");                   
                    Project = new HausarbeitAPProjectCT(createProjectDialog.NewProjectName);
                    Project.initFileListFromStack(Workspace.TempFolder);
                    Project.SaveToFile(Workspace.TempFolder + @"\project.xml");
                    loadPicture(0);
                    stackSlider.Maximum = Project.totalLayers - 1;
                    StackIsLoaded = true;
                }               
                catch (ProjectException pe)
                {
                    System.Windows.MessageBox.Show("Das Projekt konnte nicht erstellt werden\n" + pe.Message);
                    refreshSession();
                }                
            }
        }
    }
}
