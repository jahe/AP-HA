using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void btnShowCutted_Click(object sender, RoutedEventArgs e)
        {
            if (Project.section == null)
            {
                MessageBox.Show("Für dieses Projekt wurde noch keine Section gespeichert");
            }
            else
            {
                stackSlider.Value = Project.section.z;
                stackSlider.Minimum = Project.section.z;
                stackSlider.Maximum = Project.section.z + Project.section.depth;
            }            
        }
    }
}
