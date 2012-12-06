using System.Windows;
using System.Windows.Forms;

namespace AP_HA
{
    public partial class MainWindow
    {
        private void menuExitProject_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Wollen Sie das Projekt wirklich beenden?\nAlle nicht gespeicherte Projekte gehen verloren!",
                                  "Achtung",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                refreshSession();
            }                       
        }
    }
}
