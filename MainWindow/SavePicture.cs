using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace AP_HA
{
    public partial class MainWindow
    {
        public void savePicture(int picNo)
        {
            int Height = (int)this.markCanvas.ActualHeight;
            int Width = (int)this.markCanvas.ActualWidth;

            RenderTargetBitmap bmp = new RenderTargetBitmap(Width, Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(this.markCanvas);
            
            string file = Path.Combine(Workspace.TempFolder, picNo.ToString("D" + Project.filePathListBMP.Count().ToString("D").Length.ToString()) + ".bmp");

            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (Stream stm = File.Create(file))
            {
                encoder.Save(stm);
            }
        }
    }
}
