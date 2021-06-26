using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Converters
{
    public class ByteArrayConverter
    {
        public static Stream ByteArrayToStream(byte[] byteArray)
        {
            return null;
        }

        public static byte[] StreamToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
