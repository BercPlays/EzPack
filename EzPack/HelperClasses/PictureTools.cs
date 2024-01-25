using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzPack.HelperClasses
{
    static class PictureTools
    {
        public static void DrawPixelModePictureBox(Image image,PictureBox pictureBox,int quality)
        {
            try
            {
                Size sz = image.Size;

                Bitmap zoomed = (Bitmap)pictureBox.Image;
                if (zoomed != null) zoomed.Dispose();

                float zoom = (float)(quality / 4f + 1);
                zoomed = new Bitmap((int)(sz.Width * zoom), (int)(sz.Height * zoom));

                using (Graphics g = Graphics.FromImage(zoomed))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;

                    g.DrawImage(image, new Rectangle(Point.Empty, zoomed.Size));
                    g.Dispose();
                }

                pictureBox.Image = zoomed;
                image.Dispose();
            }
            catch (Exception)
            {

            }
            
        }
    }
}
