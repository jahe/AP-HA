using System.Windows;
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
        public static Bitmap AdjustBrightness(Bitmap Image, int Value)
        {
            float FinalValue = (float)Value / 255.0f;

            ColorMatrix TempMatrix = new ColorMatrix();

            TempMatrix.Matrix = new float[][]{
                                new float[] {1, 0, 0, 0, 0},
                                new float[] {0, 1, 0, 0, 0},
                                new float[] {0, 0, 1, 0, 0},
                                new float[] {0, 0, 0, 1, 0},
                                new float[] {FinalValue, FinalValue, FinalValue, 1, 1} };

            return TempMatrix.Apply(Image);
        }
    }
}
