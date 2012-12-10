using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void cutBeforeCursor(object sender, RoutedEventArgs e)    //Menü->Bearbeiten->Stapel beschneiden->Bilder vor cursor
        {
            stackSlider.Minimum = stackSlider.Value;
            loadPicture((int)stackSlider.Value);
            StackIsCutted = true;
        }
    }
}
