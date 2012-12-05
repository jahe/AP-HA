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
        #region Constructors
        public CreateProjectDialog()
        {
            InitializeComponent();
            this.ShowDialog();
        }
        #endregion

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

        private void cPDBtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(StackPath))
            {
                MessageBox.Show("Der angegebene Stapelpfad wurde nicht gefunden", "Achtung");
            }
            else if (Directory.GetFiles(StackPath, "*.tif", SearchOption.TopDirectoryOnly).Length < 1)
            {
                MessageBox.Show("Der ausgewählte Stapelpfad enthält keine geeigneten Daten. \nTIFF benötigt", "Achtung");
            }
            else if (NewProjectName.Length <= 1)
            {
                MessageBox.Show("Der angegebene Projektname ist zu kurz", "Achtung");
            }
            else if (Directory.Exists(@"C:\APHA\temp\" + NewProjectName))
            {
                MessageBox.Show("Es ist bereits ein anderes Projekt mit diesem Namen geöffnet.\nBitte wähle einen anderen Namen oder schliesse das andere Projekt", "Achtung");
            }
            else
            {
                DialogResult = true;
            }
        }
    }
}
