using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MFP.library.Enums;

namespace MFP.library.Objects.Commands.Base
{
    public abstract class BaseCommand
    {
        internal abstract CommandTypes[] CommandTypes { get; }

        internal abstract BaseCommand InitializeCommand(BinaryReader bReader, Stream stream, uint commandSize, CpuTypes cpuType);

        internal static List<BaseCommand> AssemblyCommands => typeof(BaseCommand).Assembly.GetTypes()
            .Where(a => a.BaseType == typeof(BaseCommand)).Select(b => (BaseCommand)Activator.CreateInstance(b))
            .ToList();

    }
}