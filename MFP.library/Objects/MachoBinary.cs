using System.IO;

using MFP.library.Enums;

namespace MFP.library.Objects
{
    public class MachoBinary
    {
        public MachoFormat Format { get; internal set; }

        public static MachoBinary Load(BinaryReader bReader, MachoFormat binaryFormat)
        {
            var result = new MachoBinary { Format = binaryFormat };

            return result;
        }
    }
}