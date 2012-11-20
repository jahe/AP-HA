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
using System.Drawing;
using System.Drawing.Imaging;

namespace AP_HA
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        enum Tool { ZoomIn, ZoomOut, Move };
        private Tool tool;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMarks();
        }

        Stream imageStreamSource;
        TiffBitmapDecoder decoder;
        BitmapSource bitmapSource;

        #region Programmstatus für UI-Elemente
        
        /// <summary>
        /// Event, was default-mäßig von XAML abonniert wird
        /// und somit geänderte Property-Werte mitbekommt
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke-Methode: Ruft die abonnierten Methoden des Events PropertyChanged auf
        /// --> Property xyz hat sich geändert. Aktualisiere mal den Wert!
        /// </summary>
        /// <param name="propertyName">Name des Propertys, dessen Wert sich verändert hat</param>
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Status Stapel geladen
        private bool _stackIsLoaded = false;
        public bool StackIsLoaded
        {
            get { return _stackIsLoaded; }
            set
            {
                _stackIsLoaded = value; 
                OnPropertyChanged("StackIsLoaded"); 
                CutableRight = value;                
            }
        }
        #endregion

        #region Status Bilder wurden ausgeblendet
        private bool _stackIsCutted = false;
        public bool StackIsCutted
        {
            get { return _stackIsCutted; }
            set
            {
                _stackIsCutted = value;
                OnPropertyChanged("StackIsCutted");
            }
        }
        #endregion

        #region Status Bilder am Anfang können ausgeblendet werden
        private bool _cutableLeft = false;
        public bool CutableLeft
        {
            get { return _cutableLeft; }
            set
            {
                _cutableLeft = value;
                OnPropertyChanged("CutableLeft");
            }
        }
        #endregion

        #region Status Bilder am Ende können ausgeblendet werden
        private bool _cutableRight = false;
        public bool CutableRight
        {
            get { return _cutableRight; }
            set
            {
                _cutableRight = value;
                OnPropertyChanged("CutableRight");
            }
        }
        #endregion
        #endregion
    }
}
