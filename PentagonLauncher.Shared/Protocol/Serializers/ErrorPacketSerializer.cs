namespace PentagonLauncher.Shared.Protocol.Serializers
{
    // Error response contract: success flag followed by a user-facing message.
    public static class ErrorPacketSerializer
    {
        public static byte[] CreateErrorResponsePacket(bool success, string message)
        {
            PacketWriter writer = new();
            writer.WriteBoolean(success);
            writer.WriteString(message ?? string.Empty);
            return writer.Build(Opcode.S2C_ErrorResponse);
        }

        public static (bool Success, string Message) ReadErrorResponsePacket(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_ErrorResponse);
            PacketReader reader = new(packet.Payload);
            bool success = reader.ReadBoolean();
            string message = reader.ReadString(PacketConstants.MaximumMessageByteLength);
            reader.EnsureFullyRead();
            return (success, message);
        }
    }
}
