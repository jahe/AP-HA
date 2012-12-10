using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AP_HA
{
    public partial class MainWindow
    {
        ObservableCollection<Mark> marks;

        public void InitializeMarks()
        {
            marks = new ObservableCollection<Mark>();

            // add default marks
            marks.Add(new Mark() { Name = "Herz" });
            marks.Add(new Mark() { Name = "Leber" });
            marks.Add(new Mark() { Name = "Nieren" });

            // binding
            marksListBox.ItemsSource = marks;
        }

        private void addMarkToListbutton_Click(object sender, RoutedEventArgs e)
        {
            String name = markNameTextBox.Text;
            
            if (!name.Equals(""))
            {
                marks.Add(new Mark() { Name = name });
                markNameTextBox.Clear();
            }
        }

        private void changeMarkColor(object sender, RoutedEventArgs e)
        {
            Color newColor = getColorFromDialog();

            Rectangle rect = ((Rectangle)sender);
            Mark mark = rect.DataContext as Mark;
            mark.BrushColor = new SolidColorBrush(newColor);
        }

        private Color getColorFromDialog()
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.ShowDialog();

            Color newColor = new Color();
            newColor.A = colorDialog.Color.A;
            newColor.B = colorDialog.Color.B;
            newColor.G = colorDialog.Color.G;
            newColor.R = colorDialog.Color.R;

            return newColor;
        }

        private void removeMarkFromList(object sender, RoutedEventArgs e)
        {
            Button button = ((Button) sender);
            Mark mark = button.DataContext as Mark;
            marks.Remove(mark);
        }
    }
}
