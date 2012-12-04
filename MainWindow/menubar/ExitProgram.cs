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
            DialogResult result = System.Windows.Forms.MessageBox.Show("Wollen sie die Anwendung beenden?\nAlle nicht gespeicherte Projekte gehen verloren!",
                                  "Achtung",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (Directory.Exists(@"C:\APHA\temp"))
                {
                    DataProcessor.deleteAllSubfolders(@"C:\APHA\temp");
                }
                System.Windows.Application.Current.Shutdown();
            }            
        }
    }
}
