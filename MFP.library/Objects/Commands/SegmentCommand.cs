using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

using MFP.library.Common;
using MFP.library.Enums;
using MFP.library.Objects.Commands.Base;

namespace MFP.library.Objects.Commands
{
    public class SegmentCommand : BaseCommand
    {
        public string Name { get; private set; }

        public long Address { get; private set; }

        public long Size { get; private set; }

        public long FileOffset { get; private set; }

        public long FileSize { get; private set; }

        public Protection InitialProtection { get; private set; }

        public Protection MaximalProtection { get; private set; }

        public int NumberOfSections { get; private set; }

        public ReadOnlyCollection<Section> Sections { get; private set; }

        internal override CommandTypes[] CommandTypes => new[] { Enums.CommandTypes.Segment, Enums.CommandTypes.Segment_64 };

        public byte[] Data { get; private set; }

        internal override BaseCommand LoadCommand()
        {
            Name = GetString();

            Address = ReadBitAwareInt();

            Size = ReadBitAwareInt();

            FileOffset = ReadBitAwareInt();
            FileSize = ReadBitAwareInt();

            MaximalProtection = (Protection) ReadInt32();
            InitialProtection = (Protection)ReadInt32();

            NumberOfSections = ReadInt32();

            ReadInt32();    // TODO: Flags

            if (FileSize > 0)
            {
                var streamPosition = _stream.Position;

                _stream.Seek(FileOffset, SeekOrigin.Begin);

                Data = ReadSkipBytes(checked((int)FileSize));

                _stream.Position = streamPosition;
            }

            var sections = new List<Section>();

            return this;
        }
    }
}