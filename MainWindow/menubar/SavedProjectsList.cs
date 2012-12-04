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
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSavedProjects_MouseEnter(object sender, MouseEventArgs e)
        {
            menuSavedProjects.Items.Clear();

            string[] files = Directory.GetFiles(@"C:\APHA\Projects\");

            foreach (string zip in files)
            {
                MenuItem item = new MenuItem();

                item.Header = System.IO.Path.GetFileName(zip);

                item.Click += new RoutedEventHandler(exitProgram); //TO DO Project erstellen =initializeImgList etc

                menuSavedProjects.Items.Add(item);
            }
        }
    }
}

