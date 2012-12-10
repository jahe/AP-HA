using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections;

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

            // update labels in project
            updateLabels();
        }

        private void addMarkToListbutton_Click(object sender, RoutedEventArgs e)
        {
            String name = markNameTextBox.Text;
            
            if (!name.Equals(""))
            {
                marks.Add(new Mark() { Name = name });
                markNameTextBox.Clear();
            }

            // update labels in project
            updateLabels();
        }

        private void changeMarkColor(object sender, RoutedEventArgs e)
        {
            Color newColor = getColorFromDialog();

            Rectangle rect = ((Rectangle)sender);
            Mark mark = rect.DataContext as Mark;
            mark.BrushColor = new SolidColorBrush(newColor);

            // update labels in project
            updateLabels();
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

            // update labels in project
            updateLabels();
        }

        private void displayLayerMarks(int layer)
        {
            foreach (Mark mark in marks)
            {
                foreach (KeyValuePair<int, Polyline> lpl in mark.GetLayerPolylines())
                {
                    lpl.Value.Visibility = lpl.Key == layer ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        private void updateLabels()
        {
            if (Project == null)
                return;

            ArrayList result = new ArrayList();

            foreach (Mark mark in marks)
            {
                HausarbeitAPLabelCT label = new HausarbeitAPLabelCT();

                label.id = SingleRandom.Instance.Next(1337);
                label.title = mark.Name;
                label.description = "not implemented";
                //label.color = int.Parse(mark.BrushColor.Color.ToString(), System.Globalization.NumberStyles.HexNumber);

                result.Add(label);
            }

            Project.labels = (HausarbeitAPLabelCT[])result.ToArray(typeof(HausarbeitAPLabelCT));
        }
    }
}
