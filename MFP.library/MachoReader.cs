using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MFP.library.Common;
using MFP.library.Enums;
using MFP.library.Objects;

namespace MFP.library
{
    public class MachoReader
    {
        private static MachoFormat ParseMagicBytes(uint magicBytes)
        {
            return magicBytes switch
            {
                Constants.FILEMAGIC_I386 => MachoFormat.I386,
                Constants.FILEMAGIC_AMD64 => MachoFormat.AMD64,
                Constants.FILEMAGIC_ARM64 => MachoFormat.ARM64,
                Constants.FILEMAGIC_MULTI => MachoFormat.MULTI,
                _ => MachoFormat.UNKNOWN,
            };
        }

        public static List<MachoBinary> Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File does exist", fileName);
            }

            using var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read);

            return Read(fileStream);
        }

        public static List<MachoBinary> Read(Stream stream)
        {
            using var bReader = new BinaryReader(stream, Encoding.UTF8, true);

            if (stream.Length < sizeof(uint))
            {
                return null;
            }

            var binaryFormat = ParseMagicBytes(bReader.ReadUInt32());

            switch (binaryFormat)
            {
                case MachoFormat.UNKNOWN:
                    return null;
                case MachoFormat.MULTI:
                {
                    var result = new List<MachoBinary>();

                    // TODO: Handle multiple binaries

                    return result;
                }
                case MachoFormat.I386:
                case MachoFormat.AMD64:
                case MachoFormat.ARM64:
                default:
                    return new List<MachoBinary> { MachoBinary.Load(bReader, binaryFormat) };
            }
        }
    }
}