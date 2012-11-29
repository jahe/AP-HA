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
    /// <summary>
    /// Interaktionslogik für OpenProject.xaml
    /// </summary>
    public partial class CreateProjectDialog : Window
    {
        FolderBrowserDialog openFolderDialog;
        DialogResult oFDResult;
        PictureStack pictureStack;

        #region Constructors
        public CreateProjectDialog()
        {
            InitializeComponent();
            this.ShowDialog();
        }
        
        public CreateProjectDialog(PictureStack ps)
        {
            InitializeComponent();
            //cPDStackName.Content = ps.FolderName;
            cPDWidth.Content = ps.Width;
            cPDHeight.Content = ps.Height;
            this.ShowDialog();
        }
        #endregion

        private void openStack(object sender, RoutedEventArgs e)         //Menü->Datei->Stapel laden
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
                    //refreshSession();
                    //AdjustControls.IsEnabled = true;
                    pictureStack = new PictureStack(openFolderDialog.SelectedPath);
                    //StackIsLoaded = true;
                    //stackSlider.Maximum = pictureStack.PictureAmount - 1;
                    //stackSlider.Value = 0;
                    //canvas.Width = pictureStack.Width;
                    //canvas.Height = pictureStack.Height;
                    //loadPicture(0);
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
