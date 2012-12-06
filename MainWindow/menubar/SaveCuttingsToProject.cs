using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuSaveCuttingsToProject_Click(object sender, RoutedEventArgs e)
        {
            HausarbeitAPSectionCT Section = new HausarbeitAPSectionCT();
            Section.z = (int)stackSlider.Minimum;
            Section.depth = (int)stackSlider.Maximum;
            Project.section = Section;
        }
    }
}
