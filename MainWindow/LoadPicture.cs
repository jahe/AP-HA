﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace AP_HA
{
    public partial class MainWindow
    {
        BitmapSource stackImage;

        public void loadPicture(int picNo)
        {
            try
            {
                stackImage = ImageFuncs.getImgFromPath(newProject.getPictureFromList(picNo));
                imgControl.Source = stackImage;
                canvas.Width = stackImage.PixelWidth;
                canvas.Height = stackImage.PixelHeight;
                debugTxtBox.Text = newProject.getPictureFromList(picNo);
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler bei der Bild(de)codierung\n" + e.Message + "\nAktueller Stapel muss geschlossen werden");
                refreshSession();
            }
        }
    }
}
