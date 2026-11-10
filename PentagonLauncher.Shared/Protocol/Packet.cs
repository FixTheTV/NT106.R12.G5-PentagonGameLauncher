using System;
using System.Collections.Generic;
using System.Text;
// Đại diện cho một Packet hoàn chỉnh.
namespace PentagonLauncher.Shared.Protocol
{
    public sealed class Packet
    {
        public PacketHeader Header { get; }
        public Opcode opcode => Header.Opcode;
        public byte[] Payload { get; }
        public Packet(PacketHeader header, byte[] payload)
        {
            ArgumentNullException.ThrowIfNull(payload);
            if (payload.Length != header.PayloadLength)
            {
                throw new ProtocolException($"Payload cần {header.PayloadLength} bytes, nhưng thực tế chỉ có {payload.Length} bytes");
            }
            Header = header;
            Payload = payload;
        }
    }
}
