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
using System.Windows.Threading;

namespace AP_HA
{
    /// <summary>
    /// Interaktionslogik für LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window, INotifyPropertyChanged
    {        
        private DispatcherTimer dt;
        private Random rd = new Random();
        private int i = 0;

        #region Constructors
        public LoadingWindow()
        {
            InitializeComponent();
        }

        public LoadingWindow(string text)
        {
            InitializeComponent();
            loadingText.Content = text;
            startProgressbar();
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

        private int _progressbarValue = 1;
        public int ProgressbarValue
        {
            get { return _progressbarValue; }
            set
            {
                _progressbarValue = value;
                OnPropertyChanged("ProgressbarValue");
            }
        }

        private string _percentageNo = "1";
        public string PercentageNo
        {
            get { return _percentageNo; }
            set
            {
                _percentageNo = value;
                OnPropertyChanged("PercentageNo");
            }
        }
        #endregion

        #region Progressbar
        private void startProgressbar()
        {
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dt.Start();
            dt.Tick += new EventHandler(dt_Tick);
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            if (i > 40)
            {
                dt.Stop();
                DialogResult = true;
            }
            else
            {
                int nextRd = rd.Next(1, 5);
                int addPercentage = ProgressbarValue + nextRd;  

                if (addPercentage < 100)
                {
                    ProgressbarValue = ProgressbarValue + nextRd;
                    PercentageNo = ProgressbarValue.ToString();                   
                }
                i++;
            }
        }
        #endregion
    }
}
