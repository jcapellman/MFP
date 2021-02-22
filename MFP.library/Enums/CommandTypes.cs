namespace MFP.library.Enums
{
    public enum CommandTypes : uint
    {
        DyLib = 0xc,
        WeakDyLib = 0x18,
        ReExportDyLib = 0x1f,
        LazyLoadDyLib = 0x20,
        UpwardDyLib = 0x23
    }
}