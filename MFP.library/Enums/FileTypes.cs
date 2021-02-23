namespace MFP.library.Enums
{
    public enum FileTypes : uint
    {
        Object = 0x1,
        Executable = 0x2,
        Fixed_VM_Shared_Library = 0x3,
        Core = 0x4,
        Preloaded_Executable = 0x5,
        Dynamic_Library = 0x6,
        Dynamic_Linker = 0x7,
        Bundle = 0x8,
        Dynamic_Library_Shared_Stub = 0x9,
        Companion_File_Debug_Only = 0xA,
        Kext_Bundle = 0xB
    }
}