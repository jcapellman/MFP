namespace MFP.library.Common
{
    public sealed class Section
    {
        public string Name { get; internal set; }

        public ulong Address { get; internal set; }

        public ulong Size { get; internal set; }

        public uint Offset { get; internal set; }

        public uint AlignExponent { get; internal set; }
    }
}