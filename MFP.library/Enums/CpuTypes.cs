using MFP.library.Common;

namespace MFP.library.Enums
{
    public enum CpuTypes : uint
    {
        Any = 0,
        Vax = 1,
        // 2-5 Skipped
        Motorolla68K = 6,
        x86 = 7,
        x64 = x86 | Constants.ARCHITECTURE64,
        MIPS = 8,
        // 9 Skipped
        MC98000 = 10,
        HP_PA = 11,
        ARM = 12,
        ARM64 = ARM | Constants.ARCHITECTURE64,
        MC88000 = 13,
        SPARC = 14,
        I860 = 15,
        Alpha = 16,
        // 17 Skipped
        PowerPC = 18,
        PowerPC64 = PowerPC | Constants.ARCHITECTURE64
    }
}