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
        private void menuActualProjects_MouseEnter(object sender, MouseEventArgs e)
        {
            menuActualProjects.Items.Clear();

            string[] files = Directory.GetDirectories(@"C:\APHA\temp\");

            foreach (string folder in files)
            {
                MenuItem item = new MenuItem();

                item.Header = System.IO.Path.GetFileName(folder);

                item.Click += new RoutedEventHandler(exitProgram); //TO DO Project erstellen =initializeImgList etc

                menuActualProjects.Items.Add(item);
            }
        }
    }
}
