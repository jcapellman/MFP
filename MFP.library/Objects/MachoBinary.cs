using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MFP.library.Common;
using MFP.library.Enums;
using MFP.library.Objects.Commands.Base;

namespace MFP.library.Objects
{
    public class MachoBinary
    {
        public MachoFormat Format { get; internal set; }

        public List<BaseCommand> Commands { get; internal set; }

        internal static MachoBinary Load(byte[] binary)
        {
            using var ms = new MemoryStream(binary);

            var bReader = new BinaryReader(ms);

            var format = bReader.GetFormat();

            return Load(ms, bReader, format);
        }
        
        private static List<BaseCommand> ParseCommands(BinaryReader bReader, Stream stream, int commandCount)
        {
            var commands = new List<BaseCommand>();

            var availableCommands = BaseCommand.AssemblyCommands;

            for (var x = 0; x < commandCount; x++)
            {
                var commandType = Enum.Parse<CommandTypes>(bReader.ReadUInt32().ToString());

                var commandSize = bReader.ReadUInt32();

                var command = availableCommands.FirstOrDefault(a => a.CommandTypes == commandType);

                if (command == null)
                {
                    // TODO: Error handling

                    continue;
                }
                
                commands.Add(command.InitializeCommand(bReader, stream, commandSize));
            }

            return commands;
        }

        internal static MachoBinary Load(Stream stream, BinaryReader bReader, MachoFormat binaryFormat)
        {
            var result = new MachoBinary { Format = binaryFormat };
            
            // Reset to the beginning to ensure nothing could interfere with the offsets
            stream.Seek(0, SeekOrigin.Begin);

            // TODO: Skip over for now
            bReader.ReadBytes(4);
            bReader.ReadInt32();
            bReader.ReadBytes(4);
            bReader.ReadUInt32();

            var commandCount = bReader.ReadInt32();

            // TODO: Skip over for now
            bReader.ReadInt32();
            bReader.ReadBytes(4);

            // Reserved for 64bit Binaries
            if (binaryFormat == MachoFormat.AMD64 || binaryFormat == MachoFormat.ARM64)
            {
                bReader.ReadBytes(4);
            }
            
            result.Commands = ParseCommands(bReader, stream, commandCount);
        
            return result;
        }
    }
}