using System.Windows;
using System.Windows.Input;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void btnCrop_Click(object sender, RoutedEventArgs e)
        {
            tool = Tool.CropLocation;
            if (cropRectangle.Visibility != Visibility.Visible)
            {
                zoomSlider.Value = 1.0;
                zoomExpander.IsExpanded = false;
                zoomExpander.IsEnabled = false;
                cropRectangle.Visibility = Visibility.Visible;
            }
            else
            {
                zoomExpander.IsEnabled = true;
                cropRectangle.Visibility = Visibility.Collapsed;                
            }                    
        } 
    }
}
