using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

using MFP.library.Common;
using MFP.library.Enums;
using MFP.library.Objects.Commands.Base;

namespace MFP.library.Objects.Commands
{
    public class SegmentCommand : BaseCommand
    {
        public string Name { get; private set; }

        public ulong Address { get; private set; }

        public ulong Size { get; private set; }

        public ulong FileOffset { get; private set; }

        public Protection InitialProtection { get; private set; }

        public Protection MaximalProtection { get; private set; }

        public ReadOnlyCollection<Section> Sections { get; private set; }

        internal override CommandTypes[] CommandTypes => new[] { Enums.CommandTypes.Segment, Enums.CommandTypes.Segment_64 };

        internal override BaseCommand InitializeCommand(BinaryReader bReader, Stream stream, uint commandSize)
        {
            Name = Encoding.UTF8.GetString(bReader.ReadBytes(16).TakeWhile(a => a != 0).ToArray());

            return this;
        }
    }
}