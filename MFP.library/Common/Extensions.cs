using System.IO;

using MFP.library.Enums;

namespace MFP.library.Common
{
    public static class Extensions
    {
        public static MachoFormat GetFormat(this BinaryReader bReader)
        {
            var magicBytes = bReader.ReadUInt32();

            return magicBytes switch
            {
                Constants.FILEMAGIC_I386 => MachoFormat.I386,
                Constants.FILEMAGIC_AMD64 => MachoFormat.AMD64,
                Constants.FILEMAGIC_ARM64 => MachoFormat.ARM64,
                Constants.FILEMAGIC_MULTI => MachoFormat.MULTI,
                _ => MachoFormat.UNKNOWN,
            };
        }
    }
}