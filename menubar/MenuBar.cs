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
    public partial class MainWindow //UNDER CONSTRUCTION
    {
        FolderBrowserDialog openFolderDialog;
        DialogResult oFDResult;
        PictureStack pictureStack;

        private void menuOpenFolder_Click(object sender, RoutedEventArgs e)         //Menü->Datei->Stapel laden
        {
            openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.Description = "Neuen Bildstapel auswählen";
            openFolderDialog.ShowNewFolderButton = false;                                       
            openFolderDialog.SelectedPath = @"C:\APHA\";                            

            oFDResult = openFolderDialog.ShowDialog();

            if (oFDResult == System.Windows.Forms.DialogResult.OK)                  
            {
                try     //Abfangen wenn Ordner keine geforderten Bilder enthält oder leer ist; != .tif....
                {
                    pictureStack = new PictureStack(openFolderDialog.SelectedPath);
                    menuCutStack.IsEnabled = true;
                    ContrastSlider.IsEnabled = true;
                    BrightnessSlider.IsEnabled = true;
                    stackSlider.IsEnabled = true;                                   
                    stackSlider.Maximum = pictureStack.PictureAmount-1;        
                    stackSlider.Value = 0;                   
                    loadPicture(0);                 
                }
                catch(PictureStackException ex) 
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);                   
                }                
            }
            else
            {
                debugTxtBox.Text = "Es wurde kein Ordner ausgewählt";
            }
        }

        private void menuExitProgram_Click(object sender, RoutedEventArgs e)        //Menü->Datei->Beenden
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void menuCutBeforeCursor_Click(object sender, RoutedEventArgs e)    //Menü->Bearbeiten->Stapel beschneiden->Bilder vor cursor
        {
            stackSlider.Minimum = stackSlider.Value;
            menuBackToOriginalCut.IsEnabled = true;
        }

        private void menuCutAfterCursor_Click(object sender, RoutedEventArgs e)     //Menü->Bearbeiten->Stapel beschneiden->Bilder nach cursor
        {
            stackSlider.Maximum = stackSlider.Value;
            menuBackToOriginalCut.IsEnabled = true;
        }

        private void menuBackToOriginalCut_Click(object sender, RoutedEventArgs e)  //Menü->Bearbeiten
        {
            stackSlider.Minimum = 0;
            stackSlider.Maximum = pictureStack.PictureAmount - 1;
            menuBackToOriginalCut.IsEnabled = false;
        }
    }
}
