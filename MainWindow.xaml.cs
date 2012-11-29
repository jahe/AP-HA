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
        enum Tool { ZoomIn, ZoomOut, Move, None };

        enum Wheel { Up, Down, None };
        enum MouseMovement { Left, Up, Right, Down, None };

        private Tool? tool = Tool.Move;
        private List<Key> keys;
        private List<MouseButton> mouseButtons;
        private Wheel mouseWheel;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMarks();

            keys = new List<Key>();
            mouseButtons = new List<MouseButton>();
            mouseWheel = Wheel.None;

            this.KeyDown += new KeyEventHandler(OnKeyDown);                         // KeyDown Event abonieren
            this.KeyUp += new KeyEventHandler(OnKeyUp);                             // KeyUp Event abonieren
            this.PreviewMouseDown += new MouseButtonEventHandler(OnPreviewMouseDown); // MouseDown Event abonieren
            this.PreviewMouseUp += new MouseButtonEventHandler(OnPreviewMouseUp);   // MouseUp Event abonieren
            this.PreviewMouseWheel += new MouseWheelEventHandler(OnPreviewMouseWheel);
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

            if (pictureStack != null)
            {
                pictureStack.stackReset();
            }
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

        private void ResetBrightnessBtn_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            this.debugTxtBox.Text = e.Key.ToString();

            if (!keys.Contains(e.Key))
                keys.Add(e.Key);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            this.debugTxtBox.Text = e.Key.ToString();

            keys.Remove(e.Key);
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.debugTxtBox.Text = e.ChangedButton.ToString();

            if (!mouseButtons.Contains(e.ChangedButton))
                mouseButtons.Add(e.ChangedButton);
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtons.Remove(e.ChangedButton);
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                mouseWheel = Wheel.Up;
            else if (e.Delta < 0)
                mouseWheel = Wheel.Down;
            else
                mouseWheel = Wheel.None;

            this.debugTxtBox.Text = mouseWheel.ToString();
        }

        private void menuMouseSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingWindow = new Settings();
            settingWindow.Show();

        }
    }
}
