#region Usings

using System.Drawing;
using System.Drawing.Imaging;

#endregion


namespace AP_HA
{
    public class ColorMatrix
    {
        #region Constructors

        public ColorMatrix()
        {

        }
        #endregion

        public float[][] Matrix { get; set; }

        public Bitmap Apply(Bitmap OriginalImage)
        {
            Bitmap NewBitmap = new Bitmap(OriginalImage.Width, OriginalImage.Height);

            using (Graphics NewGraphics = Graphics.FromImage(NewBitmap))
            {
                System.Drawing.Imaging.ColorMatrix NewColorMatrix = new System.Drawing.Imaging.ColorMatrix(Matrix);

                using (ImageAttributes Attributes = new ImageAttributes())
                {
                    Attributes.SetColorMatrix(NewColorMatrix);

                    NewGraphics.DrawImage(OriginalImage,
                       new System.Drawing.Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                       0, 0, OriginalImage.Width, OriginalImage.Height,
                       GraphicsUnit.Pixel,
                       Attributes);
                }
            }

            return NewBitmap;
        }
    }
}
