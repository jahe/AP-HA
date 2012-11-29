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
        #region Constructos
        public CreateProjectDialog()
        {
            this.Show();
        }
        
        public CreateProjectDialog(PictureStack ps)
        {
            InitializeComponent();
            cPDPicAmount.Content = ps.PictureAmount;
            cPDStackName.Content = ps.FolderName;
            cPDWidth.Content = ps.Width;
            cPDHeight.Content = ps.Height;
            this.Show();
        }
        #endregion
    }
}
