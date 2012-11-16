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
using System.ComponentModel;

namespace AP_HA
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        enum Tool { ZoomIn, ZoomOut, Move };
        private Tool tool;

        #region Programmstatus für UI-Elemente
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #region Status Stapel geladen

        private bool stackIsLoaded = false;

        public bool StackIsLoaded
        {
            get { return this.stackIsLoaded; }
        }

        public void stackLoaded(bool stats)
        {
            stackIsLoaded = stats;
            //OnPropertyChanged("StackIsLoaded");
        }
        #endregion

        #region Status Bilder wurden ausgeblendet

        private bool stackIsCutted = false;

        public bool StackIsCutted
        {
            get { return this.stackIsCutted; }
        }

        public void stackCutted(bool stats)
        {
            stackIsCutted = stats;
            OnPropertyChanged("StackIsCutted");
        }
        #endregion

        #region Status Bilder am Anfang können ausgeblendet werden

        private bool cutableLeft = false;

        public bool CutableLeft
        {
            get { return this.cutableLeft; }
        }

        public void isCutableLeft(bool stats)
        {
            cutableLeft = stats;
            OnPropertyChanged("CutableLeft");
        }
        #endregion

        #region Status Bilder am Anfang können ausgeblendet werden

        private bool cutableRight = false;

        public bool CutableRight
        {
            get { return this.cutableRight; }
        }

        public void isCutableRight(bool stats)
        {
            cutableRight = stats;
            OnPropertyChanged("CutableRight");
        }
        #endregion       
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            InitializeMarks();
        }

        public void loadPicture(int picNo)
        {
            Stream imageStreamSource = new FileStream(pictureStack.getPictureFromList(picNo), FileMode.Open, FileAccess.Read, FileShare.Read);
            TiffBitmapDecoder decoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];

            imgControl.Source = bitmapSource;
            debugTxtBox.Text = pictureStack.getPictureFromList(picNo);
        }
    }
}
