using System;
using System.IO;
using System.Text;

using MFP.library.Enums;
using MFP.library.Objects.Commands.Base;

namespace MFP.library.Objects.Commands
{
    public class DyLibCommand : BaseCommand
    {
        private const int NamePropertyOffset = 24;

        public long TimeStamp { get; private set; }

        public long CurrentVersion { get; private set; }
        
        public long CompatibilityVersion { get; private set; }
        
        public string Name { get; private set; }


        internal override CommandTypes[] CommandTypes => new[]
        {
            Enums.CommandTypes.Load_Dynamic_Library
        };

        internal override BaseCommand InitializeCommand(BinaryReader bReader, Stream stream, uint commandSize, CpuTypes cpuType)
        {
            // TODO: Skip for now
            bReader.ReadBytes(4);

            TimeStamp = bReader.ReadInt32();

            CurrentVersion = bReader.ReadInt32();

            CompatibilityVersion = bReader.ReadInt32();

            Name = Encoding.UTF8.GetString(bReader.ReadBytes(Convert.ToInt32(commandSize) - NamePropertyOffset)).TrimEnd('\0');

            return this;
        }
    }
}