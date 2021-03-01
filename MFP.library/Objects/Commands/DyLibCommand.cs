using System;

using MFP.library.Enums;
using MFP.library.Objects.Commands.Base;

namespace MFP.library.Objects.Commands
{
    public class DyLibCommand : BaseCommand
    {
        private const int NamePropertyOffset = 24;

        public ulong TimeStamp { get; private set; }

        public int CurrentVersion { get; private set; }
        
        public long CompatibilityVersion { get; private set; }
        
        public string Name { get; private set; }


        internal override CommandTypes[] CommandTypes => new[]
        {
            Enums.CommandTypes.Load_Dynamic_Library
        };

        internal override BaseCommand LoadCommand()
        {
            // TODO: Skip for now
            SkipBytes();

            TimeStamp = ReadUnsignedInt();

            CurrentVersion = ReadInt32();

            CompatibilityVersion = ReadInt32();

            Name = GetString(Convert.ToInt32(CommandSize) - NamePropertyOffset);

            return this;
        }
    }
}