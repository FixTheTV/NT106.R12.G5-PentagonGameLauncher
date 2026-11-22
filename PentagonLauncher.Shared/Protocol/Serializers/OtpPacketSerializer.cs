using PentagonLauncher.Shared.Models.Authentication;

namespace PentagonLauncher.Shared.Protocol.Serializers;

public static class OtpPacketSerializer
{
    public static byte[] CreateResult(Opcode opcode, OtpResult result)
    {
        var writer = new PacketWriter();
        writer.WriteBoolean(result.Success);
        writer.WriteString(result.Message);
        return writer.Build(opcode);
    }

    public static OtpResult ReadResult(Packet packet, Opcode expected)
    {
        PacketGuard.EnsureOpcode(packet, expected);
        var reader = new PacketReader(packet.Payload);
        var result = new OtpResult { Success = reader.ReadBoolean(), Message = reader.ReadString(PacketConstants.MaximumMessageByteLength) };
        reader.EnsureFullyRead();
        return result;
    }
}
