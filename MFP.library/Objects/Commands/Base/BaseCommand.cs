using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MFP.library.Enums;

namespace MFP.library.Objects.Commands.Base
{
    public abstract class BaseCommand
    {
        private BinaryReader _binaryReader;

        private CpuTypes _cpuTypes;

        private Stream _stream;

        private uint _commandSize;

        internal abstract CommandTypes[] CommandTypes { get; }

        internal BaseCommand InitializeCommand(BinaryReader bReader, Stream stream, uint commandSize, CpuTypes cpuType)
        {
            _binaryReader = bReader;

            _stream = stream;

            _commandSize = commandSize;

            _cpuTypes = cpuType;

            return LoadCommand();
        }

        internal abstract BaseCommand LoadCommand();

        internal static List<BaseCommand> AssemblyCommands => typeof(BaseCommand).Assembly.GetTypes()
            .Where(a => a.BaseType == typeof(BaseCommand)).Select(b => (BaseCommand)Activator.CreateInstance(b))
            .ToList();
    }
}