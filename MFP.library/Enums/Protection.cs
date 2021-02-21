using System;

namespace MFP.library.Enums
{
    [Flags]
    public enum Protection
    {
        Read = 1,
        Write = 2,
        Execute = 4
    }
}