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
using System.Windows.Shapes;

namespace AP_HA
{
    /// <summary>
    /// Interaktionslogik für OpenProject.xaml
    /// </summary>
    public partial class CreateProjectDialog : Window
    {
        public CreateProjectDialog(int picAmount)
        {
            InitializeComponent();
            cPDPicAmount.Content = picAmount.ToString();
        }
    }
}
