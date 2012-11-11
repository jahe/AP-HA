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
            openFolderDialog.ShowNewFolderButton = false;                           //Anwender verbieten neuen Ordner zu erstellen            
            openFolderDialog.SelectedPath = @"C:\APHA\";                            //Vorgabe Pfad

            oFDResult = openFolderDialog.ShowDialog();

            if (oFDResult == System.Windows.Forms.DialogResult.OK)                  //Wenn im Dialog OK-Button gedrückt wurde
            {
                try                                                                 //Abfangen wenn Ordner keine geforderten Bilder enthält oder leer ist; != .tif....
                {
                    pictureStack = new PictureStack(openFolderDialog.SelectedPath);
                    debugTxtBox.Text = "Im gewählten Ordner " + pictureStack.getPath() + " befinden sich: " + pictureStack.getPictureAmount() + " Dateien";
                }
                catch
                {
                    //TO DO Individuelle Ausgabe der Fehlermeldung durch Exceptions
                    System.Windows.MessageBox.Show("Der ausgewählte Ordner entspricht nicht den Anforderungen o.ä.");
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
    }
}
