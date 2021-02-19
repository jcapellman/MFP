using System.IO;

using MFP.library.Common;
using MFP.library.Enums;

namespace MFP.library.Objects
{
    public class MachoBinary
    {
        public MachoFormat Format { get; internal set; }

        public static MachoBinary Load(byte[] binary)
        {
            using var ms = new MemoryStream(binary);

            var bReader = new BinaryReader(ms);

            var format = bReader.GetFormat();

            return Load(bReader, format);
        }

        public static MachoBinary Load(BinaryReader bReader, MachoFormat binaryFormat)
        {
            var result = new MachoBinary { Format = binaryFormat };

            return result;
        }
    }
}