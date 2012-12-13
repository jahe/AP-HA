using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AP_HA
{
    public partial class MainWindow
    {
        public void savePicture(int picNo)
        {
            if (polylineStack.Count == 0)
                return;
            
            int Height = (int)this.Project.height;
            int Width = (int)this.Project.width;

            RenderTargetBitmap bmp = new RenderTargetBitmap(Width, Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(this.markCanvas);
            
            string file = Path.Combine(Workspace.TempFolder, picNo.ToString("D" + Project.filePathListBMP.Count().ToString("D").Length.ToString()) + ".bmp");

            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (Stream stm = File.Create(file))
            {
                encoder.Save(stm);
            }

            polylineStack.Clear();
            markCanvas.Children.RemoveRange(2, markCanvas.Children.Count - 2);
        }
    }
}
