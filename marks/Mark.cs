using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AP_HA
{
    class Mark : INotifyPropertyChanged
    {
        string name;
        bool visible;
        SolidColorBrush brushColor;
        ArrayList polylines;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public SolidColorBrush BrushColor
        {
            get { return brushColor; }
            set
            {
                brushColor = value;
                NotifyPropertyChanged("BrushColor"); 
                
                // change color of existing polylines
                foreach (Polyline pl in polylines)
                {
                    pl.Stroke = brushColor;
                }
            }
        }

        public Mark()
        {
            polylines = new ArrayList();
            Visible = true;
            BrushColor = getRandomColorBrush();
        }

        private SolidColorBrush getRandomColorBrush()
        {
            byte[] colorBytes = new byte[3];
            SingleRandom.Instance.NextBytes(colorBytes);
            Color randomColor = Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);
            return new SolidColorBrush(randomColor);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void AddPolyline(Polyline polyline)
        {
            polylines.Add(polyline);
        }
    }
}
