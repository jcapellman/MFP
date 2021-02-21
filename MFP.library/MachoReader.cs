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
        /// <summary>
        /// Attempts to load the Macho file provided
        /// </summary>
        /// <param name="fileName">Full path to the Macho</param>
        /// <returns>Null if invalid, a List collection of MachoBinary files if valid</returns>
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

        /// <summary>
        /// Attempts to load the Macho file supplied via a stream
        /// </summary>
        /// <param name="stream">Valid stream to a Macho Binary</param>
        /// <returns>Null if invalid, a List collection of MachoBinary files if valid</returns>
        public static List<MachoBinary> Read(Stream stream)
        {
            using var bReader = new BinaryReader(stream, Encoding.UTF8, true);

            if (stream.Length < sizeof(uint))
            {
                return null;
            }

            var binaryFormat = bReader.GetFormat();

            return binaryFormat switch
            {
                MachoFormat.UNKNOWN => null,
                MachoFormat.MULTI => FatBinary.Load(stream),
                _ => new List<MachoBinary> { MachoBinary.Load(bReader, binaryFormat) },
            };
        }
    }
}