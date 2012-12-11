using System;
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
using System.Drawing;

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
        private Button ok;
        private ComboBox mouseMoveCB;
        private Button btnDownSection;

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

            StackPanel scsPanel = new StackPanel();
            scsPanel.Orientation = Orientation.Horizontal;

            scText = new TextBox();
            scText.FontSize = 30;
            scText.Margin = new Thickness(5,20,5,20);
            scText.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scText.IsManipulationEnabled = false;

            btnDownSection = new Button();
            btnDownSection.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.WhiteSmoke);
            btnDownSection.Width = 40;
            btnDownSection.Height = 40;
            btnDownSection.IsEnabled = true;

            Label mmLbl = new Label();
            mmLbl.Content = "Maus Bewegung: ";

            mouseMoveCB = new ComboBox();
            mouseMoveCB.Items.Add(MouseMoveDirection.None);
            mouseMoveCB.Items.Add(MouseMoveDirection.Up);
            mouseMoveCB.Items.Add(MouseMoveDirection.Down);
            mouseMoveCB.Items.Add(MouseMoveDirection.Left);
            mouseMoveCB.Items.Add(MouseMoveDirection.Right);
            mouseMoveCB.SelectedIndex = 0;

            scsPanel.Children.Add(mmLbl);
            scsPanel.Children.Add(mouseMoveCB);

            btnPanel = new StackPanel();
            btnPanel.Orientation = Orientation.Horizontal;
            reset = new Button();
            reset.Content = "Zurücksetzen";
            ok = new Button();
            ok.Content = "OK";
            reset.Click += OnReset;
            ok.Click += OnOk;

            btnPanel.Children.Add(reset);
            btnPanel.Children.Add(ok);

            sPanel.Children.Add(header);
            sPanel.Children.Add(scText);
            sPanel.Children.Add(btnDownSection);
            sPanel.Children.Add(scsPanel);
            sPanel.Children.Add(btnPanel);

            this.AddChild(sPanel);

            this.PreviewKeyDown += new KeyEventHandler(OnKeyDown);
            btnDownSection.PreviewMouseDown += new MouseButtonEventHandler(OnMouseDown);
            this.PreviewMouseWheel += new MouseWheelEventHandler(OnPreviewMouseWheel);
            //this.PreviewMouseMove += new MouseEventHandler(OnPreviewMouseMove);
            mouseMoveCB.SelectionChanged += new SelectionChangedEventHandler(OnMmSelectChanged);
        }

        private void OnMmSelectChanged(object sender, SelectionChangedEventArgs e)
        {
            sc.register((MouseMoveDirection) mouseMoveCB.SelectedItem);
            scText.Text = sc.ToString();
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            sc.reset();
            mouseMoveCB.SelectedIndex = 0;
            scText.Text = sc.ToString();
        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            sc.register(e.Key);
            scText.Text = sc.ToString();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
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

        private System.Windows.Point? lastMovePoint;

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(this);

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
