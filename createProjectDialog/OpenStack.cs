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
        private void openStack(object sender, RoutedEventArgs e)         //CreateProjectDialog->Stapel öffnen
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.Description = "Neuen Bildstapel auswählen";
            openFolderDialog.ShowNewFolderButton = false;
            openFolderDialog.SelectedPath = @"C:\APHA\";

            DialogResult oFDResult = openFolderDialog.ShowDialog();

            if (oFDResult == System.Windows.Forms.DialogResult.OK)
            {
                try     //Abfangen wenn Ordner keine geforderten Bilder enthält oder leer ist; != .tif....
                {
                    //ALT pictureStack = new PictureStack(openFolderDialog.SelectedPath); 
                    //NEU Property StackPath = openFolderDialog.SelectedPath
                }
                catch (PictureStackException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //debugTxtBox.Text = "Es wurde kein Ordner ausgewählt";
            }
        }
    }
}
