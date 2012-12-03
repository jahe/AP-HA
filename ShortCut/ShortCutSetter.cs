using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace AP_HA
{
    class ShortCutSetter : ContentControl
    {
        private StackPanel sPanel;
        private TextBlock header;
        private TextBox scText;
        private StackPanel btnPanel;
        private Button reset;
        private Button cancel;
        private Button ok;

        public ShortCutSetter()
            : base()
        {
            Width = 100;
            Height = 50;

            sPanel = new StackPanel();
            sPanel.Orientation = Orientation.Vertical;

            header = new TextBlock();
            header.FontWeight = FontWeights.Bold;
            header.Text = "Neuer Shortcut:";

            scText = new TextBox();
            scText.IsEnabled = false;

            btnPanel = new StackPanel();
            btnPanel.Orientation = Orientation.Horizontal;
            reset = new Button();
            cancel = new Button();
            ok = new Button();
            btnPanel.Children.Add(reset);
            btnPanel.Children.Add(reset);

            sPanel.Children.Add(header);
            sPanel.Children.Add(scText);
        }
    }
}
