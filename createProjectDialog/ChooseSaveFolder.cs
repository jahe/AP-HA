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
using System.ComponentModel;

namespace AP_HA
{
    public partial class CreateProjectDialog
    {
        private void chooseSaveFolder(object sender, RoutedEventArgs e) //CreateProjectDialog -> Zielordner auswählen
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.Description = "Speicherort auswählen";
            openFolderDialog.ShowNewFolderButton = true;
            openFolderDialog.SelectedPath = @"C:\APHA\";

            DialogResult oFDResult = openFolderDialog.ShowDialog();

            if (oFDResult == System.Windows.Forms.DialogResult.OK)
            {
                SaveProjectPath = openFolderDialog.SelectedPath;
            }
            else
            {
                //Es wurde kein Ordner ausgewählt
            }
        }
    }
}
