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
    /// Interaktionslogik für OpenProject.xaml
    /// </summary>
    public partial class CreateProjectDialog : Window, INotifyPropertyChanged
    {
        #region Properties für UI-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Dateipfad zum Bilderstapel
        private string _stackPath;
        public string StackPath
        {
            get { return _stackPath; }
            set
            {
                _stackPath = value;
                OnPropertyChanged("StackPath");
            }
        }
        #endregion

        #region Zielpfad um Project zu speichern
        private string _saveProjectPath;
        public string SaveProjectPath
        {
            get { return _saveProjectPath; }
            set
            {
                _saveProjectPath = value;
                OnPropertyChanged("SaveProjectPath");
            }
        }
        #endregion

        #region Name für neues Projekt
        private string _newProjectName;
        public string NewProjectName
        {
            get { return _newProjectName; }
            set
            {
                _newProjectName = value;
                OnPropertyChanged("NewProjectName");
            }
        }
        #endregion
        #endregion

        #region Constructors
        public CreateProjectDialog()
        {
            InitializeComponent();
            this.ShowDialog();
        }
        #endregion

        private void cPDBtnOK_Click(object sender, RoutedEventArgs e) //Fehlererkennung evtl über Exceptions
        {
            if (!Directory.Exists(StackPath))
            {
                MessageBox.Show("Der angegebene Stapelpfad wurde nicht gefunden");
            }
            else if (Directory.GetFiles(StackPath, "*.tif", SearchOption.TopDirectoryOnly).Length < 1)
            {
                MessageBox.Show("Der ausgewählte Stapelpfad enthält keine geeigneten Daten. TIFF benötigt");
            }
            else if (NewProjectName.Length <= 1)
            {
                MessageBox.Show("Der angegebene Projektname ist zu kurz");
            }
            else if (!Directory.Exists(SaveProjectPath))
            {
                MessageBox.Show("Der angegebene Zielpfad wurde nicht gefunden");
            }
            else
            {
                MessageBox.Show("Aus dem Ordner: " + StackPath + " wird das Projekt mit dem Namen " + NewProjectName + " in " + SaveProjectPath + " erstellt");
                DialogResult = true;
            }           
        }      
    }
}
