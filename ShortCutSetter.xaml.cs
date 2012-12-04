﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AP_HA
{
    /// <summary>
    /// Interaktionslogik für ShortCutSetter.xaml
    /// </summary>
    public partial class ShortCutSetter : Window
    {
        private ShortCut sc;
        private StackPanel sPanel;
        private TextBlock header;
        private TextBox scText;
        private StackPanel btnPanel;
        private Button reset;
        private Button cancel;
        private Button ok;

        public ShortCutSetter(ShortCut sc)
        {
            this.sc = sc;
            InitializeComponent();
            InitializePresentation();
        }

        public void InitializePresentation()
        {
            sc.reset();    // Werte des Shortcuts zurücksetzen

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
            reset.Content = "Zurücksetzen";
            cancel = new Button();
            cancel.Content = "Abbrechen";
            ok = new Button();
            ok.Content = "OK";
            reset.Click += OnReset;
            cancel.Click += OnCancel;
            ok.Click += OnOk;

            btnPanel.Children.Add(reset);
            btnPanel.Children.Add(cancel);
            btnPanel.Children.Add(ok);

            sPanel.Children.Add(header);
            sPanel.Children.Add(scText);
            sPanel.Children.Add(btnPanel);

            this.AddChild(sPanel);

            this.PreviewKeyDown += new KeyEventHandler(OnKeyDown);
            this.PreviewMouseDown += new MouseButtonEventHandler(OnPreviewMouseDown);
            this.PreviewMouseWheel += new MouseWheelEventHandler(OnPreviewMouseWheel);
            this.PreviewMouseMove += new MouseEventHandler(OnPreviewMouseMove);
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {

        }
        private void OnReset(object sender, RoutedEventArgs e)
        {
            sc.reset();
        }
        private void OnOk(object sender, RoutedEventArgs e)
        {

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            sc.register(e.Key);
            scText.Text = sc.ToString();
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            sc.register(e.ChangedButton);
            scText.Text = sc.ToString();
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                sc.register(Wheel.Up);
            else if (e.Delta < 0)
                sc.register(Wheel.Down);
            else
                sc.register(Wheel.None);

            scText.Text = sc.ToString();
        }

        private Point? lastMovePoint;

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(this);

            if (lastMovePoint != null)
            {
                double deltaX = mousePos.X - lastMovePoint.Value.X;
                double deltaY = mousePos.Y - lastMovePoint.Value.Y;

                MouseMoveDirection moveDir = MouseMoveDirection.None;

                if (Math.Abs(deltaX) > Math.Abs(deltaY))        // Maus ist eher zur Seite gegangen
                {
                    if (deltaX < 0)
                        moveDir = MouseMoveDirection.Left;
                    else
                        moveDir = MouseMoveDirection.Right;
                }
                else if (Math.Abs(deltaX) < Math.Abs(deltaY))
                {
                    if (deltaY < 0)
                        moveDir = MouseMoveDirection.Up;
                    else
                        moveDir = MouseMoveDirection.Down;
                }

                sc.register(moveDir);
                scText.Text = sc.ToString();

                lastMovePoint = mousePos;
            }
            else
            {
                lastMovePoint = mousePos;
            }
        }
    }
}