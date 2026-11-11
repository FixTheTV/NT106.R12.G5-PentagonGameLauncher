using System;
using System.Collections.Generic;
using System.Text;
// Tạo và đọc ping, pong, phụ trách Opcode 1000, 2000.
namespace PentagonLauncher.Shared.Protocol
{
    public static class ConnectionPacketSerializer
    {
        public static byte[] CreatePingPacket()
        {
            PacketWriter writer = new();
            return writer.Build(Opcode.S2C_Ping);
        }
        public static void ReadPingPacket(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_Ping);
            PacketGuard.EnsureEmptyPayload(packet);
        }

        public static byte[] CreatePongPacket()
        {
            PacketWriter writer = new();
            return writer.Build(Opcode.C2S_Pong);
        }
        public static void ReadPongPacket(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_Pong);
            PacketGuard.EnsureEmptyPayload(packet);
        }
    }

}
