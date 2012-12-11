using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Xml.Serialization;

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
        private static String rootAppFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private string workspaceFolder = @"C:\APHA\workspace";
        private HausarbeitAPProjectCT Project;
        private Workspace Workspace;

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            InitializeMarks();
            //createDefaultSce(@"C:\Users\admin\Desktop");
            DataProcessor.deleteAllSubfolders(workspaceFolder); //\Workspace\ leeren
            InitializeShortcuts();
        }
        #endregion

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

        #region Status Section wird angezeigt
        private bool _sectionView = false;
        public bool SectionView
        {
            get { return _sectionView; }
            set
            {
                _sectionView = value;
                OnPropertyChanged("SectionView");
            }
        }
        #endregion

        #region Wenn Stapel geladen wurde
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

        #region Text füt Statusbox
        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }
        #endregion

        #region Text füt Projectbox
        private string _projectText;
        public string ProjectText
        {
            get { return _projectText; }
            set
            {
                _projectText = value;
                OnPropertyChanged("ProjectText");
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

        private void registerShortcutFuncs()
        {
            if (scEngine != null)
            {
                // HIER VLLT NOCH ALLE EVENTS DER SCs DEABONNIEREN ( also = null setzen )
                scEngine.getShortcutFromName("Zoom In").Execute += zoomIn;
                scEngine.getShortcutFromName("Zoom Out").Execute += zoomOut;
                scEngine.getShortcutFromName("Scroll In").Execute += scrollIn;
                scEngine.getShortcutFromName("Scroll Out").Execute += scrollOut;
                scEngine.getShortcutFromName("Increase Brightness").Execute += incBrightness;
                scEngine.getShortcutFromName("Decrease Brightness").Execute += decBrightness;
                scEngine.getShortcutFromName("Increase Contrast").Execute +=  incContrast;
                scEngine.getShortcutFromName("Decrease Contrast").Execute += decContrast;
            }
        }

        private static void createDefaultSce(String destFolderPath)
        {
            ShortCutEngine tempSce = new ShortCutEngine();

            // ********* Shortcuts erzeugen ***********
            ShortCut zoomInSc = new ShortCut("Zoom In");
            zoomInSc.register(Key.LeftCtrl);
            zoomInSc.register(Wheel.Up);

            ShortCut zoomOutSc = new ShortCut("Zoom Out");
            zoomOutSc.register(Key.LeftCtrl);
            zoomOutSc.register(Wheel.Down);

            ShortCut scrollInSc = new ShortCut("Scroll In");
            scrollInSc.register(Key.LeftCtrl);
            scrollInSc.register(MouseMoveDirection.Up);

            ShortCut scrollOutSc = new ShortCut("Scroll Out");
            scrollOutSc.register(Key.LeftCtrl);
            scrollOutSc.register(MouseMoveDirection.Down);

            ShortCut incBrightnessSc = new ShortCut("Increase Brightness");
            incBrightnessSc.register(Key.B);
            incBrightnessSc.register(Wheel.Up);

            ShortCut decBrightnessSc = new ShortCut("Decrease Brightness");
            decBrightnessSc.register(Key.B);
            decBrightnessSc.register(Wheel.Down);

            ShortCut incContrastSc = new ShortCut("Increase Contrast");
            incContrastSc.register(Key.C);
            incContrastSc.register(Wheel.Up);

            ShortCut decContrastSc = new ShortCut("Decrease Contrast");
            decContrastSc.register(Key.C);
            decContrastSc.register(Wheel.Down);

            // ***** Shortcuts der Engine hinzufügen *****
            tempSce.addShortCut(zoomInSc);
            tempSce.addShortCut(zoomOutSc);
            tempSce.addShortCut(scrollInSc);
            tempSce.addShortCut(scrollOutSc);
            tempSce.addShortCut(incBrightnessSc);
            tempSce.addShortCut(decBrightnessSc);
            tempSce.addShortCut(incContrastSc);
            tempSce.addShortCut(decContrastSc);

            tempSce.Serialize(destFolderPath + @"\default.sce");
        }

        private void InitializeShortcuts()
        {
            sc = new ShortCut();    // actual Shortcut, der gerade im MainWindow gedrückt/aktiv ist
            
            this.PreviewKeyDown += new KeyEventHandler(OnKeyDown);
            this.PreviewKeyUp += new KeyEventHandler(OnKeyUp);
            this.PreviewMouseDown += new MouseButtonEventHandler(OnPreviewMouseDown);
            this.PreviewMouseUp += new MouseButtonEventHandler(OnPreviewMouseUp);
            this.PreviewMouseWheel += new MouseWheelEventHandler(OnPreviewMouseWheel);
            this.PreviewMouseMove += new MouseEventHandler(OnPreviewMouseMove);

            String SceFilePath = rootAppFolder + @"\ShortCut\default.sce";

            //if (!File.Exists(SceFilePath))
            //{
            //    File.Copy(rootAppFolder + @"\ShortCut\default.sce", SceFilePath);
            //}
            try
            {
                scEngine = ShortCutEngine.Deserialize(SceFilePath);
                registerShortcutFuncs();
                ShortCutChanged += new ShortCutHandler(scEngine.onShortCutChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void refreshSession()
        {
            canvas.Width = 0;
            canvas.Height = 0;
            //AdjustControls.IsEnabled = false;
            stackSlider.Value = 0;
            Project = new HausarbeitAPProjectCT();
            Workspace = new Workspace();
            StackIsLoaded = false;
            StackIsCutted = false;
            SectionView = false;
            imgControl.Source = null;
            StatusText = "Bitte einen Stapel oder Projekt öffnen";
            ProjectText = "";
            BrightnessSlider.Value = 0.0;
            ContrastSlider.Value = 1.0;
            zoomSlider.Value = 1.0;
            this.Title = "JPBM-BodyViewer";
            DataProcessor.deleteAllSubfolders(workspaceFolder);
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
            settingsWindow = new Settings(scEngine);
            settingsWindow.ShowDialog();

            try
            {
                scEngine.Serialize(rootAppFolder + @"\ShortCut\default.sce");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void cropRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(tool != Tool.CropSize)
            {
                tool = Tool.CropSize;
            }           
        }

        private void GroupBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (tool == Tool.CropSize)
            {
                tool = Tool.None;
            } 
        }

                              

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int Height = (int)this.markCanvas.ActualHeight;
            int Width = (int)this.markCanvas.ActualWidth;

            RenderTargetBitmap bmp = new RenderTargetBitmap(Width, Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(this.markCanvas);

            string file = rootAppFolder + @"\render.bmp";

            string Extension = System.IO.Path.GetExtension(file).ToLower();

            BitmapEncoder encoder;
            encoder = new BmpBitmapEncoder();
            /*
            if (Extension == ".gif")
                encoder = new GifBitmapEncoder();
            else if (Extension == ".png")
                encoder = new PngBitmapEncoder();
            else if (Extension == ".jpg")
                encoder = new JpegBitmapEncoder();
            else
                return;
             * */

            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (Stream stm = File.Create(file))
            {
                encoder.Save(stm);
            }
        }

        private void cropBtnLocate(object sender, RoutedEventArgs e)
        {
            tool = Tool.CropLocation;
        }       
    }
}
