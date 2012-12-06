using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSavedProjects_MouseEnter(object sender, MouseEventArgs e)
        {
            menuSavedProjects.Items.Clear();

            string[] files = Directory.GetFiles(@"C:\APHA\Projects\", "*.zip");

            foreach (string zip in files)
            {
                MenuItem item = new MenuItem();

                item.Header = System.IO.Path.GetFileNameWithoutExtension(zip);
                item.Click += new EventHandler(openProject); //TO DO Project erstellen =initializeImgList etc
                menuSavedProjects.Items.Add(item);
            }
        }       
    }
}

