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
using System.Diagnostics;

namespace AP_HA
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private delegate void ShortCutHandler(object sender, ShortCutEventArgs e);
        private event ShortCutHandler ShortCutChanged;
        private ShortCutEngine scEngine;
        private ShortCut sc;
        private Tool? tool = Tool.Move;
        private Settings settingsWindow;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMarks();

            this.PreviewKeyDown += new KeyEventHandler(OnKeyDown);                         // KeyDown Event abonieren
            this.PreviewKeyUp += new KeyEventHandler(OnKeyUp);                             // KeyUp Event abonieren
            this.PreviewMouseDown += new MouseButtonEventHandler(OnPreviewMouseDown); // MouseDown Event abonieren
            this.PreviewMouseUp += new MouseButtonEventHandler(OnPreviewMouseUp);   // MouseUp Event abonieren
            this.PreviewMouseWheel += new MouseWheelEventHandler(OnPreviewMouseWheel);
            this.PreviewMouseMove += new MouseEventHandler(OnPreviewMouseMove);

            sc = new ShortCut();
            scEngine = new ShortCutEngine();
            ShortCutChanged += new ShortCutHandler(scEngine.onShortCutChanged);

            // ********* Shortcuts erzeugen ***********
            ShortCut zoomInSc = new ShortCut("Zoom In");
            zoomInSc.register(Key.LeftCtrl);
            zoomInSc.register(Wheel.Up);
            zoomInSc.Execute += new ExecuteHandler(zoomIn);

            ShortCut zoomOutSc = new ShortCut("Zoom Out");
            zoomOutSc.register(Key.LeftCtrl);
            zoomOutSc.register(Wheel.Down);
            zoomOutSc.Execute += new ExecuteHandler(zoomOut);

            ShortCut scrollInSc = new ShortCut("Scroll In");
            scrollInSc.register(Key.LeftCtrl);
            scrollInSc.register(MouseMoveDirection.Up);
            scrollInSc.Execute += new ExecuteHandler(scrollIn);

            ShortCut scrollOutSc = new ShortCut("Scroll Out");
            scrollOutSc.register(Key.LeftCtrl);
            scrollOutSc.register(MouseMoveDirection.Down);
            scrollOutSc.Execute += new ExecuteHandler(scrollOut);

            ShortCut incBrightnessSc = new ShortCut("Increase Brightness");
            incBrightnessSc.register(Key.B);
            incBrightnessSc.register(Wheel.Up);
            incBrightnessSc.Execute += new ExecuteHandler(incBrightness);

            ShortCut decBrightnessSc = new ShortCut("Decrease Brightness");
            decBrightnessSc.register(Key.B);
            decBrightnessSc.register(Wheel.Down);
            decBrightnessSc.Execute += new ExecuteHandler(decBrightness);

            ShortCut incContrastSc = new ShortCut("Increase Contrast");
            incContrastSc.register(Key.C);
            incContrastSc.register(Wheel.Up);
            incContrastSc.Execute += new ExecuteHandler(incContrast);

            ShortCut decContrastSc = new ShortCut("Decrease Contrast");
            decContrastSc.register(Key.C);
            decContrastSc.register(Wheel.Down);
            decContrastSc.Execute += new ExecuteHandler(decContrast);
            
            // ***** Shortcuts der Engine hinzufügen *****
            scEngine.addShortCut(zoomInSc);
            scEngine.addShortCut(zoomOutSc);
            scEngine.addShortCut(scrollInSc);
            scEngine.addShortCut(scrollOutSc);
            scEngine.addShortCut(incBrightnessSc);
            scEngine.addShortCut(decBrightnessSc);
            scEngine.addShortCut(incContrastSc);
            scEngine.addShortCut(decContrastSc);
        }

        #region Properties für UI-Binding
        
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

        private void refreshSession()
        {
            //AdjustControls.IsEnabled = false;
            stackSlider.Value = 0;
            StackIsLoaded = false;
            StackIsCutted = false;
            imgControl.Source = null;
            debugTxtBox.Text = "Bitte einen Stapel öffnen";
            BrightnessSlider.Value = 0.0;
            ContrastSlider.Value = 1.0;
            zoomSlider.Value = 1.0;

           /** if (pictureStack != null)
            {
                pictureStack.stackReset();
            }**/
        }

        private void ResetBrightnessBtn_Click(object sender, RoutedEventArgs e)
        {
            BrightnessSlider.Value = 0.0;
        }

        private void ResetContrastBtn_Click(object sender, RoutedEventArgs e)
        {
            ContrastSlider.Value = 1.0;
        }

        private void ResetZoomBtn_Click(object sender, RoutedEventArgs e)
        {
            zoomSlider.Value = 1.0;
        }

        private void menuMouseSettings_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow = new Settings();
            settingsWindow.ShowDialog();
        }
    }
}
