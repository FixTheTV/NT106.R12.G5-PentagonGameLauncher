using PentagonLauncher.Shared.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
// Tạo đọc lỗi protocol/ server, phụ trách Opcode 9000.
namespace PentagonLauncher.Shared.Protocol
{
    public static class ErrorPacketSerializer
    {
        public static byte[] CreateErrorResponsePacket(ErrorResponse response)
        {
            ArgumentNullException.ThrowIfNull(response);
            PacketWriter writer = new();
            writer.WriteUInt16((ushort)response.ErrorCode);
            writer.WriteString(response.Message);

            return writer.Build(Opcode.S2C_ErrorResponse);
        }

        public static ErrorResponse ReadErrorResponsePacket(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_ErrorResponse);
            PacketReader reader = new(packet.Payload);
            ushort ErrorCodeValue = reader.ReadUInt16();
            string message = reader.ReadString(PacketConstants.MaximumMessageByteLength);
            reader.EnsureFullyRead();

            ProtocolErrorCode errorCode = (ProtocolErrorCode)ErrorCodeValue;
            if (!Enum.IsDefined(typeof(ProtocolErrorCode), errorCode))
            {
                throw new ProtocolException($"Error code khong duoc ho tro : {ErrorCodeValue}");
            }

            return new ErrorResponse
            {
                ErrorCode = errorCode,
                Message = message
            };
        }
    }
}
