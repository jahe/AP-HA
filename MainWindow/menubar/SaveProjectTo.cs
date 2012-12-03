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
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSaveProjectAs_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            DialogResult fBDResult = new DialogResult();
            fBD.ShowDialog();

            if (fBDResult == System.Windows.Forms.DialogResult.OK)
            {
                newProject.createZipFromStack(newWorkspace.TempFolder, fBD.SelectedPath);
            }
        }
    }
}
