﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace AP_HA
{
    public partial class MainWindow
    {
        public static Bitmap AdjustContrast(Bitmap Image, double value)
        {
            float FinalValue = (float)(1 - value) / 2;
            float tempValue = (float)value;
            ColorMatrix TempMatrix = new ColorMatrix();

            TempMatrix.Matrix = new float[][]{
                                new float[] {tempValue, 0, 0, 0, 0},
                                new float[] {0, tempValue, 0, 0, 0},
                                new float[] {0, 0, tempValue, 0, 0},
                                new float[] {0, 0, 0, tempValue, 0},
                                new float[] {FinalValue, FinalValue, FinalValue, 1, 1} };

            return TempMatrix.Apply(Image);
        }
    }
}