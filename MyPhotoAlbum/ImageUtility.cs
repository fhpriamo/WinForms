using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace MyPhotoAlbum
{
    public class ImageUtility
    {
        public static Rectangle ScaleToFit(Bitmap bmp, Rectangle targetArea)
        {
            Rectangle result = new Rectangle(
            targetArea.Location, targetArea.Size);

            // Determine best fit: width or height
            if (result.Height * bmp.Width > result.Width * bmp.Height)
            {
                // Final width should match target,
                // determine and center height
                result.Height = result.Width * bmp.Height / bmp.Width;
                result.Y += (targetArea.Height - result.Height) / 2;
            }
            else
            {
                // Final height should match target,
                // determine and center width
                result.Width = result.Height * bmp.Width / bmp.Height;
                result.X += (targetArea.Width - result.Width) / 2;
            }

            return result;
        }
    }
}
