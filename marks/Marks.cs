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

                label.id = SingleRandom.Instance.Next(1337 * 42);
                label.title = mark.Name;
                label.description = "not implemented";
                label.color = colorToInt(mark.BrushColor.Color);
                
                result.Add(label);
            }

            Project.labels = (HausarbeitAPLabelCT[])result.ToArray(typeof(HausarbeitAPLabelCT));
        }

        private void loadLabels()
        {
            marks.Clear();

            foreach (HausarbeitAPLabelCT label in Project.labels)
            {
                Mark mark = new Mark();
                mark.Name = label.title;
                mark.BrushColor.Color = intToColor(label.color);
                marks.Add(mark);
            }
        }

        private int colorToInt(Color input)
        {
            System.Drawing.Color myColor = System.Drawing.Color.FromArgb(input.A, input.R, input.G, input.B);
            string hexColor = System.Drawing.ColorTranslator.ToHtml(myColor).Substring(1);
            return Convert.ToInt32(hexColor, 16);
        }

        private Color intToColor(int input)
        {
            string hexColor = string.Format("{0:x}", input);
            System.Drawing.Color myColor = System.Drawing.ColorTranslator.FromHtml("#" + hexColor);
            return System.Windows.Media.Color.FromArgb(myColor.A, myColor.R, myColor.G, myColor.B);
        }
    }
}
