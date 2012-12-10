using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void cutAfterCursor(object sender, RoutedEventArgs e)     //Menü->Bearbeiten->Stapel beschneiden->Bilder nach cursor
        {
            stackSlider.Maximum = stackSlider.Value;
            loadPicture((int)stackSlider.Value);
            StackIsCutted = true;
        }
    }
}
