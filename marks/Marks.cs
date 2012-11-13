using System;
using System.Collections.Generic;
using System.Windows;

namespace AP_HA
{
    public partial class MainWindow
    {
        List<Mark> marks;

        public void InitializeMarks()
        {
            marks = new List<Mark>();

            // add default marks
            marks.Add(new Mark() { Name = "Herz" });
            marks.Add(new Mark() { Name = "Leber" });
            marks.Add(new Mark() { Name = "Nieren" });

            // binding
            marksListBox.ItemsSource = marks;
        }
    }
}
