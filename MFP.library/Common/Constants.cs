namespace MFP.library.Common
{
    internal class Constants
    {
        public const uint FILEMAGIC_I386  = 0xFEEDFACE;

        public const uint FILEMAGIC_AMD64 = 0xFEEDFACF;

        public const uint FILEMAGIC_ARM64 = 0xFEEDFACD;

        public const uint FILEMAGIC_MULTI = 0xBEBAFECA;

        public const uint CPUTYPE_ARM64 = 16777228;

        public const uint CPUTYPE_AMD64 = 16777223;

        public const int FAT_HEADER_SIZE = 20;

        public const int ARCHITECTURE64 = 0x1000000;
    }
}