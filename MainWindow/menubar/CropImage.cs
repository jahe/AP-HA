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
                cropRectangle.Visibility = Visibility.Visible;
            }
            else
            {
                cropRectangle.Visibility = Visibility.Collapsed;                
            }                    
        } 
    }
}
