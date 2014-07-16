using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMap_Ver1.Model;

namespace TiledMap_Ver1.Object
{
    public static class Util
    {
        private static Image sourceImage;

        public static Bitmap bmp;

        public static Image SourceImage
        {
            get { return Util.sourceImage; }
            set { Util.sourceImage = value; }
        }
        /// <summary>
        /// Crop image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        public static Image Crop(Rectangle selection)
        {
            // Check if it is a bitmap:
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");

            // Crop the image:
           return bmp.Clone(selection, bmp.PixelFormat);
        }

        /// <summary>
        /// Compare 2 Image by using Memory Stream
        /// </summary>
        /// <param name="i1">Image 1</param>
        /// <param name="i2">image 2</param>
        /// <returns></returns>
        public static bool ImageCompare(Image i1, Image i2)
        {
            MemoryStream ms = new MemoryStream();
            i1.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            String strI1 = Convert.ToBase64String(ms.ToArray());
            ms.Position = 0;

            i2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            String strI2 = Convert.ToBase64String(ms.ToArray());

            if (strI1.Equals(strI2))
            {
                ms.Dispose();
                return true;
            }
            else
            {
                ms.Dispose();
                return false;
            }

        }

        

    }
}
