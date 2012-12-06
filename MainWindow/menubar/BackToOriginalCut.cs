using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void backToOriginalCut(object sender, RoutedEventArgs e)  //Menü->Bearbeiten
        {
            stackSlider.Minimum = 0;
            stackSlider.Maximum = Project.totalLayers - 1;
            StackIsCutted = false;
        }
    }
}
