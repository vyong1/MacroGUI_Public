using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ImageProcessing
{
    // TODO [Low Priority] - Handle non-windows OS more gracefully

    public class ImageCalc
    {
        public static double Luminance(Bitmap bm, Rectangle? rect = null)
        {
            if (!OperatingSystem.IsWindows())
            {
                return 0;
            }

            double lum = 0;
            var tmpBmp = new Bitmap(bm);
            var width = bm.Width;
            var height = bm.Height;
            var bppModifier = bm.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;
            
            if (rect == null)
            {
                // Use the whole bitmap unless specified
                rect = new Rectangle(0, 0, bm.Width, bm.Height);
            }

            var srcData = tmpBmp.LockBits(rect.Value, ImageLockMode.ReadOnly, bm.PixelFormat);
            var stride = srcData.Stride;
            var scan0 = srcData.Scan0;

            //Luminance (standard, objective): (0.2126*R) + (0.7152*G) + (0.0722*B)
            //Luminance (perceived option 1): (0.299*R + 0.587*G + 0.114*B)
            //Luminance (perceived option 2, slower to calculate): sqrt( 0.299*R^2 + 0.587*G^2 + 0.114*B^2 )

            unsafe
            {
                byte* p = (byte*)(void*)scan0;

                for (int y = 0; y < rect.Value.Height; y++)
                {
                    for (int x = 0; x < rect.Value.Width; x++)
                    {
                        int idx = (y * stride) + x * bppModifier;
                        lum += (0.299 * p[idx + 2] + 0.587 * p[idx + 1] + 0.114 * p[idx]);
                    }
                }
            }

            tmpBmp.UnlockBits(srcData);
            tmpBmp.Dispose();
            var avgLum = lum / (width * height);


            return avgLum / 255.0;
        }
    }
}
