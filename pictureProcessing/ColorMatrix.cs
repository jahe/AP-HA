#region Usings

using System.Drawing;
using System.Drawing.Imaging;

#endregion

  
namespace AP_HA
{

     /// <summary>

    /// Helper class for setting up and applying a color matrix

     /// </summary>

     public class ColorMatrix

    {
        #region Constructor

  

          /// <summary>

          /// Constructor

           /// </summary>

           public ColorMatrix()

           {

           }

   

          #endregion

  

          #region Properties

 

         /// <summary>

          /// Matrix containing the values of the ColorMatrix

         /// </summary>

         public float[][] Matrix { get; set; }

  

        #endregion

   

        #region Public Functions

 

          /// <summary>

          /// Applies the color matrix

         /// </summary>

        /// <param name="OriginalImage">Image sent in</param>

        /// <returns>An image with the color matrix applied</returns>

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

   

         #endregion

     }

  }
