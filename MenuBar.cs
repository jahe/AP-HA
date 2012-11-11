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
using Microsoft.Win32;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        FolderBrowserDialog openFolderDialog;

        private void menuOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.Description = "Neuen Bildstapel auswählen";
            openFolderDialog.ShowNewFolderButton = false;                          //Anwender verbieten neuen Ordner zu erstellen            
            openFolderDialog.SelectedPath = @"C:\";                                //Vorgabe Pfad

            DialogResult objResult = openFolderDialog.ShowDialog();

            if (objResult == System.Windows.Forms.DialogResult.OK)
            {
                try                                                                //Abfangen wenn Ordner keine geforderten Bilder enthält oder leer ist; != .tif....
                {
                    PictureStack pictureStack = new PictureStack(openFolderDialog.SelectedPath);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Der ausgewählte Ordner entspricht nicht den Anforderungen");
                }
                
            }
            else
            {
                debugTxtBox.Text = "Es wurde kein Ordner ausgewählt";
            }
        }

        private void menuExitProgram_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
