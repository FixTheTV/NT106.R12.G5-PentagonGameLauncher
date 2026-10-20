using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
// Đọc / ghi length và opcode, cố định Header luôn là 6 bytes.
namespace PentagonLauncher.Shared.Protocol
{
    public readonly struct PacketHeader
    {
        public int TotalLength { get; }
        public Opcode Opcode { get; }
        public int PayloadLength => TotalLength - PacketConstants.HeaderSize;
        public PacketHeader(int totalLength, Opcode opcode)
        {
            if (totalLength < PacketConstants.HeaderSize)
            {
                throw new ProtocolException(
                    $"Packet length không hợp lệ : {TotalLength} .");
            }
            if (totalLength > PacketConstants.HeaderSize)
            {
                throw new ProtocolException(
                    $"Packet vượt quá giới hạn {PacketConstants.MaximumPackerSize} bytes ");
            }
            if (!Enum.IsDefined(typeof(Opcode), opcode))
            {
                throw new ProtocolException(
                    $"Opcode không được hỗ trợ {(ushort)opcode}");
            }
            TotalLength = totalLength;
            Opcode = opcode;
        }
        public static PacketHeader Parse(ReadOnlySpan<byte> headerBytes)
        {
            if (headerBytes.Length != PacketConstants.HeaderSize)
            {
                throw new ProtocolException(
                    $"Header phải có đúng {PacketConstants.HeaderSize}");
            }
            int totalLength = BinaryPrimitives.ReadInt32LittleEndian(headerBytes[..4]);
            ushort opcodeValue = BinaryPrimitives.ReadUInt16LittleEndian(headerBytes.Slice(4, 2));
            return new PacketHeader(totalLength, (Opcode)opcodeValue);
        }
        public void WriteTo(Span<byte> destination)
        {
            if (destination.Length < PacketConstants.HeaderSize)
            {
                throw new ProtocolException($"Buffer không đủ chỗ để ghi Header.");
            }
            BinaryPrimitives.WriteInt32LittleEndian(destination[..4], TotalLength);
            BinaryPrimitives.WriteUInt16LittleEndian(destination.Slice(4, 2), (ushort)Opcode);
        }
    }
}
