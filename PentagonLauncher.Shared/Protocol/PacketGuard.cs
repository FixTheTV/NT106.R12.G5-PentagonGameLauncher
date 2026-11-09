using System;
using System.Collections.Generic;
using System.Text;
// Kiểm tra opcode và payload chung.
namespace PentagonLauncher.Shared.Protocol
{
    internal static class PacketGuard
    {
        public static void EnsureOpcode(Packet packet, Opcode expectedOP)
        {
            ArgumentNullException.ThrowIfNull(packet);
            if (packet.opcode != expectedOP)
            {
                throw new ProtocolException($"Packet opcode là {packet.opcode}, nhưng cần {expectedOP}");
            }

        }
        public static void EnsureEmptyPayload(Packet packet)
        {
            if (packet.Payload.Length != 0)
            {
                throw new ProtocolException($"Packet {packet.Payload} khong duoc co Payload.");
            }
        }
    }
}
