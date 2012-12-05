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

namespace AP_HA
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private ShortCutEngine scEngine;

        private Grid grid;

        public Settings(ShortCutEngine sce)
        {
            scEngine = sce;
            
            InitializeComponent();
            InitializePresentation();
        }

        private void InitializePresentation()
        {
            mainGrid.Children.Remove(grid);
            grid = new Grid();

            RowDefinition row0 = new RowDefinition();
            ColumnDefinition col0 = new ColumnDefinition();
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();

            grid.RowDefinitions.Add(row0);
            grid.ColumnDefinitions.Add(col0);
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);

            TextBlock funcHead = new TextBlock();
            funcHead.FontWeight = FontWeights.Bold;
            funcHead.Text = "Funktion";
            TextBlock ScHead = new TextBlock();
            ScHead.FontWeight = FontWeights.Bold;
            ScHead.Text = "Shortcut";

            Grid.SetColumn(funcHead, 0);
            Grid.SetRow(funcHead, 0);
            Grid.SetColumn(ScHead, 1);
            Grid.SetRow(ScHead, 0);

            grid.Children.Add(funcHead);
            grid.Children.Add(ScHead);

            if (scEngine != null)
            {
                foreach (ShortCut sc in scEngine.ShortCuts)
                {
                    RowDefinition tempRow = new RowDefinition();
                    grid.RowDefinitions.Add(tempRow);

                    TextBlock funcName = new TextBlock();
                    funcName.Text = sc.Name;
                    Grid.SetColumn(funcName, 0);
                    Grid.SetRow(funcName, scEngine.ShortCuts.IndexOf(sc) + 1);

                    TextBlock shortcutText = new TextBlock();
                    shortcutText.Text = sc.ToString();
                    Grid.SetColumn(shortcutText, 1);
                    Grid.SetRow(shortcutText, scEngine.ShortCuts.IndexOf(sc) + 1);

                    Button setShortcut = new Button();
                    setShortcut.Tag = sc;
                    setShortcut.Content = "Neu";
                    setShortcut.Click += setShortcut_Click;
                    Grid.SetColumn(setShortcut, 2);
                    Grid.SetRow(setShortcut, scEngine.ShortCuts.IndexOf(sc) + 1);

                    grid.Children.Add(funcName);
                    grid.Children.Add(shortcutText);
                    grid.Children.Add(setShortcut);
                }
            }

            mainGrid.Children.Add(grid);
        }

        private void setShortcut_Click(object sender, RoutedEventArgs e)
        {
            ShortCut sc = (ShortCut)((Button)sender).Tag;
            ShortCutSetter scs = new ShortCutSetter(sc);
            scs.ShowDialog();

            updateShortcuts();
        }

        private void updateShortcuts()
        {
            InitializePresentation();
        }
    }
}
