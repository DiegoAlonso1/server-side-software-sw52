using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Converters
{
    public static class BitmapConverter
    {
        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        public static Bitmap ByteArrayToBitmap(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return (Bitmap)Image.FromStream(ms);
            }
        }
    }
}
