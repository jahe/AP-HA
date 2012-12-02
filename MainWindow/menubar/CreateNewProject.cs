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
        private void menuCreateProject_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectDialog createProjectDialog = new CreateProjectDialog();

            if (createProjectDialog.DialogResult.HasValue && createProjectDialog.DialogResult.Value)
            {
                try
                {
                    HausarbeitAPProjectCT newProject = new HausarbeitAPProjectCT(createProjectDialog.NewProjectName);
                    newProject.SaveToFile("test");
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
