using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AP_HA
{
    class Mark : INotifyPropertyChanged
    {
        string name;
        bool visible;
        SolidColorBrush brushColor;
        ArrayList layerPolylines;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set {
                visible = value;

                // change visibility of existing polylines
                foreach (KeyValuePair<int, Polyline> lpl in layerPolylines)
                {
                    lpl.Value.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        public SolidColorBrush BrushColor
        {
            get { return brushColor; }
            set
            {
                brushColor = value;
                NotifyPropertyChanged("BrushColor"); 
                
                // change color of existing polylines
                foreach (KeyValuePair<int, Polyline> lpl in layerPolylines)
                {
                    lpl.Value.Stroke = brushColor;
                }
            }
        }

        public Mark()
        {
            layerPolylines = new ArrayList();
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

        public void AddPolyline(int layer, Polyline polyline)
        {
            KeyValuePair<int, Polyline> layerPolyline = new KeyValuePair<int, Polyline>(layer, polyline);
            layerPolylines.Add(layerPolyline);
        }
    }
}
