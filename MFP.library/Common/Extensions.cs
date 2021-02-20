using System.IO;

using MFP.library.Enums;

namespace MFP.library.Common
{
    public static class Extensions
    {
        public static MachoFormat GetFormat(this BinaryReader bReader)
        {
            var magicBytes = bReader.ReadUInt32();

            switch (magicBytes)
            {
                case Constants.FILEMAGIC_I386:
                    return MachoFormat.I386;
                case Constants.FILEMAGIC_MULTI:
                    return MachoFormat.MULTI;
                case Constants.FILEMAGIC_AMD64:
                case Constants.FILEMAGIC_ARM64:
                    var cpuType = bReader.ReadUInt32();

                    switch (cpuType)
                    {
                        case Constants.CPUTYPE_AMD64:
                            return MachoFormat.AMD64;
                        case Constants.CPUTYPE_ARM64:
                            return MachoFormat.ARM64;
                        default:
                            return MachoFormat.UNKNOWN;
                    }
                default:
                    return MachoFormat.UNKNOWN;
            }
        }
    }
}