using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace AP_HA
{
    class Mark
    {
        string name;
        bool visible;
        SolidColorBrush brushColor;

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
            set { brushColor = value; }
        }

        public Mark()
        {
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
    }
}
