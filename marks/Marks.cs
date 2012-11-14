﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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

        private void removeMarkFromList(object sender, RoutedEventArgs e)
        {
            Button button = ((Button) sender);
            Mark mark = button.DataContext as Mark;
            marks.Remove(mark);
        }
    }
}
