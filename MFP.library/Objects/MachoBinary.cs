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

        public CpuTypes CpuType { get; internal set; }

        public FileTypes FileType { get; internal set; }

        public Flags Flags { get; internal set; }

        public int CommandSize { get; internal set; }

        public List<BaseCommand> Commands { get; internal set; }

        internal static MachoBinary Load(byte[] binary)
        {
            using var ms = new MemoryStream(binary);

            var bReader = new BinaryReader(ms);

            var format = bReader.GetFormat();

            return Load(ms, bReader, format);
        }
        
        private static List<BaseCommand> ParseCommands(BinaryReader bReader, Stream stream, int commandCount, CpuTypes cpuType)
        {
            var commands = new List<BaseCommand>();

            var availableCommands = BaseCommand.AssemblyCommands;

            for (var x = 0; x < commandCount; x++)
            {
                var commandTypeInt = bReader.ReadUInt32();

                if (!Enum.IsDefined(typeof(CommandTypes), commandTypeInt))
                {
                    // TODO: Error Handling

                    continue;
                }

                var commandType = (CommandTypes) commandTypeInt;

                var commandSize = bReader.ReadUInt32();

                var command = availableCommands.FirstOrDefault(a => a.CommandTypes.Contains(commandType));

                if (command == null)
                {
                    // TODO: Error handling

                    continue;
                }
                
                commands.Add(command.InitializeCommand(bReader, stream, commandSize, cpuType));
            }

            return commands;
        }

        internal static MachoBinary Load(Stream stream, BinaryReader bReader, MachoFormat binaryFormat)
        {
            var result = new MachoBinary { Format = binaryFormat };
            
            // Reset to the beginning right after the binaryFormat 4 bytes
            stream.Seek(4, SeekOrigin.Begin);
            
            result.CpuType = (CpuTypes)bReader.ReadInt32();
            bReader.ReadBytes(4); // TODO: CPU Subtype Mapping

            result.FileType = (FileTypes) bReader.ReadUInt32();

            var commandCount = bReader.ReadInt32();
            
            result.CommandSize = bReader.ReadInt32();
            bReader.ReadBytes(4); // TODO: Flags

            // Reserved for 64bit Binaries
            if (result.CpuType.IsCpu64bit())
            {
                bReader.ReadBytes(4); // TODO: Store?
            }
            
            result.Commands = ParseCommands(bReader, stream, commandCount, result.CpuType);
        
            return result;
        }
    }
}