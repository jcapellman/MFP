using System;
using System.IO;
using System.Net;

using MFP.library.Enums;

namespace MFP.library.Common
{
    public class EndianReader
    {
        private readonly bool _endianAdjustment;
        
        private readonly Stream _stream;

        public EndianReader(Stream stream, Endian endian)
        {
            this._stream = stream;
            _endianAdjustment = endian == Endian.LITTLE ^ BitConverter.IsLittleEndian;
        }

        public byte[] ReadBytes(int count)
        {
            var result = new byte[count];

            _stream.Read(result, result.Length - count, count);

            return result;
        }

        public byte ReadByte() => (byte)_stream.ReadByte();

        public short ReadInt16()
        {
            var value = BitConverter.ToInt16(ReadBytes(2), 0);

            if (_endianAdjustment)
            {
                value = IPAddress.NetworkToHostOrder(value);
            }

            return value;
        }

        public ushort ReadUInt16() => (ushort)ReadInt16();

        public int ReadInt32()
        {
            var value = BitConverter.ToInt32(ReadBytes(4), 0);

            if (_endianAdjustment)
            {
                value = IPAddress.NetworkToHostOrder(value);
            }

            return value;
        }

        public uint ReadUInt32()
        {
            return (uint)ReadInt32();
        }
    }
}