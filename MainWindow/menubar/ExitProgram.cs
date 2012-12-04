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
using Microsoft.Win32;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void exitProgram(object sender, RoutedEventArgs e)        //Menü->Datei->Beenden
        {
            string strMeldung = "Wollen Sie die Anwendung beenden?";
            DialogResult result = System.Windows.Forms.MessageBox.Show(strMeldung,
                                  System.Windows.Forms.Application.ProductName,
                                   MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (Directory.Exists(@"C:\APHA\temp"))
                {
                    Workspace.deleteAllSubfolders(@"C:\APHA\temp");
                }
                System.Windows.Application.Current.Shutdown();
            }            
        }
    }
}
