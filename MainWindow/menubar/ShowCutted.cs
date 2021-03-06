﻿using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void btnShowCutted_Click(object sender, RoutedEventArgs e)
        {
            if (Project.section == null)
            {
                MessageBox.Show("Für dieses Projekt wurde noch keine Section angelegt", "Achtung");
            }
            else
            {
                if(Project.section.z > 0)
                {
                    stackSlider.Value = Project.section.z;
                    stackSlider.Minimum = Project.section.z;                   
                }
                if(Project.section.depth > 0 && Project.section.z > 0)
                {
                    stackSlider.Maximum = Project.section.depth + Project.section.z;
                }
                SectionView = true;                
                loadPicture((int)stackSlider.Value);
            }            
        }
    }
}
