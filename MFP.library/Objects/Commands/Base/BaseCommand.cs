﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MFP.library.Common;
using MFP.library.Enums;

namespace MFP.library.Objects.Commands.Base
{
    public abstract class BaseCommand
    {
        private BinaryReader _binaryReader;

        private CpuTypes _cpuTypes;

        private Stream _stream;

        protected uint CommandSize;

        internal abstract CommandTypes[] CommandTypes { get; }

        internal BaseCommand InitializeCommand(BinaryReader bReader, Stream stream, uint commandSize, CpuTypes cpuType)
        {
            _binaryReader = bReader;

            _stream = stream;

            CommandSize = commandSize;

            _cpuTypes = cpuType;

            return LoadCommand();
        }

        internal abstract BaseCommand LoadCommand();

        protected string GetString(int byteCount = 16) => Encoding.UTF8.GetString(_binaryReader.ReadBytes(byteCount).TakeWhile(a => a != 0).ToArray());

        protected ulong ReadUnsignedInt() => _cpuTypes.IsCpu64bit() ? _binaryReader.ReadUInt64() : _binaryReader.ReadUInt32();

        protected int ReadInt32() => _binaryReader.ReadInt32();

        protected void SkipBytes(int skipBytes = 4) => _binaryReader.ReadBytes(skipBytes);

        internal static List<BaseCommand> AssemblyCommands => typeof(BaseCommand).Assembly.GetTypes()
            .Where(a => a.BaseType == typeof(BaseCommand)).Select(b => (BaseCommand)Activator.CreateInstance(b))
            .ToList();
    }
}