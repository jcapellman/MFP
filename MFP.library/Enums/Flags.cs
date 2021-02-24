namespace MFP.library.Enums
{
    public enum Flags
    {
        No_Defined_References = 0x1,
        Incremental_Link = 0x2,
        Dynamic_Linker = 0x4,
        Bind_at_Load = 0x8,
        Prebound = 0x10,
        Split_Segments = 0x20,
        Lazy_Initialization = 0x40,
        Two_Level = 0x80,
        Force_Flat = 0x100,
        No_Fix_Prebinding = 0x200,
        Prebindable = 0x400,
        All_Modules_Bound = 0x800,
        Subsections_Via_Symbols = 0x1000,
        Canonical = 0x2000,
        Weak_Defines = 0x4000,
        Binds_to_Weak = 0x8000,
        Allow_Stack_Execution = 0x10000,
        Root_Safe = 0x20000,
        SetUid_Safe = 0x40000,
        No_Exported_Dynamic_Library = 0x80000,
        Pie = 0x100000,
        Dead_Strippable_Dynamic_Library = 0x200000,
        Has_TLV_Descriptors = 0x400000,
        No_Heap_Execution = 0x800000
    }
}