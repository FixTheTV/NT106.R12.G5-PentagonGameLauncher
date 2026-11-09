using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
// ghi primitive thành bytes , chịu trách nhiệm ghi bool, Int32, uint16, string
namespace PentagonLauncher.Shared.Protocol
{
    public sealed class PacketWriter
    {
        private readonly MemoryStream _payloadStream = new();
        public void WriteByte(byte value)
        {
            _payloadStream.WriteByte(value);
        }

        // bool luôn chiếm đúng một byte: 0 = false, 1 = true
        public void WriteBoolean(bool value)
        {
            WriteByte(value ? (byte)1 : (byte)0);
        }

        public void WriteUInt16(ushort value)
        {
            Span<byte> buffer = stackalloc byte[2];
            BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);
            _payloadStream.Write(buffer);
        }

        public void WriteInt32(int value)
        {
            Span<byte> buffer = stackalloc byte[4];
            BinaryPrimitives.WriteInt32LittleEndian(buffer, value);
            _payloadStream.Write(buffer);
        }

        public void WriteString(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(value);
            if (utf8Bytes.Length > ushort.MaxValue)
            {
                throw new ProtocolException(($"String quá dài : {utf8Bytes.Length} bytes"));
            }
            WriteUInt16((ushort)utf8Bytes.Length);
            _payloadStream.Write(utf8Bytes);
        }

        public byte[] Build(Opcode opcode)
        {
            if (!Enum.IsDefined(typeof(Opcode), opcode))
            {
                throw new ProtocolException($"Opcode không được hỗ trợ :{(ushort)opcode}.");
            }
            byte[] payload = _payloadStream.ToArray();
            int totalLength = PacketConstants.HeaderSize + payload.Length;
            PacketHeader header = new(totalLength, opcode);
            byte[] packetBytes = new byte[totalLength];
            header.WriteTo(packetBytes);
            payload.CopyTo(packetBytes.AsSpan(PacketConstants.HeaderSize));
            return packetBytes;
        }
    }
}
