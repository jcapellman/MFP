using System.Collections.Generic;
using System.IO;

using MFP.library.Common;
using MFP.library.Enums;

namespace MFP.library.Objects
{
    internal class FatBinary
    {
        internal static List<MachoBinary> Load(Stream stream)
        {
            var binaries = new List<MachoBinary>();

            var eReader = new EndianReader(stream, Endian.Big);

            var binaryCount = eReader.ReadUInt32();
            
            var startPosition = stream.Position;

            for (var idx = 0; idx < binaryCount; idx++)
            {
                stream.Seek(startPosition + Constants.FAT_HEADER_SIZE * idx + 8, SeekOrigin.Begin);

                var offset = eReader.ReadInt32();

                var size = eReader.ReadInt32();

                var result = new byte[size];

                stream.Seek(offset, SeekOrigin.Begin);

                stream.Read(result, 0, size);

                binaries.Add(MachoBinary.Load(result));
            }

            return binaries;
        }
    }
}