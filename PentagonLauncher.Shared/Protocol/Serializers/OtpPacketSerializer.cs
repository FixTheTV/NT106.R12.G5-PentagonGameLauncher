using PentagonLauncher.Shared.Models.Authentication;

namespace PentagonLauncher.Shared.Protocol.Serializers;

public static class OtpPacketSerializer
{
    // Giữ nguyên wire format: purpose (1 byte), email.
    public static byte[] CreateRegisterRequest(RegisterOTPRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        var writer = new PacketWriter();
        writer.WriteByte((byte)request.Purpose);
        writer.WriteString(request.Email);
        return writer.Build(Opcode.C2S_RegisterOTPRequest);
    }

    public static RegisterOTPRequest ReadRegisterRequest(Packet packet)
    {
        PacketGuard.EnsureOpcode(packet, Opcode.C2S_RegisterOTPRequest);
        var reader = new PacketReader(packet.Payload);
        var purpose = (OTP.OtpPurpose)reader.ReadByte();
        string email = reader.ReadString(PacketConstants.MaximunEmailByteLength);
        reader.EnsureFullyRead();
        return new RegisterOTPRequest { Purpose = purpose, Email = email };
    }

    // Hai opcode xác minh dùng cùng layout: email, OTPCode, purpose.
    public static byte[] CreateVerificationRequest(Opcode opcode, RegisterOTPVerificationRequest request)
    {
        EnsureVerificationOpcode(opcode);
        ArgumentNullException.ThrowIfNull(request);
        var writer = new PacketWriter();
        writer.WriteString(request.Email);
        writer.WriteString(request.OTPCode);
        writer.WriteByte((byte)request.Purpose);
        return writer.Build(opcode);
    }

    public static RegisterOTPVerificationRequest ReadVerificationRequest(Packet packet, Opcode expected)
    {
        EnsureVerificationOpcode(expected);
        PacketGuard.EnsureOpcode(packet, expected);
        var reader = new PacketReader(packet.Payload);
        string email = reader.ReadString(PacketConstants.MaximunEmailByteLength);
        string code = reader.ReadString(PacketConstants.MaximumOtpcodeByteLength);
        var purpose = (OTP.OtpPurpose)reader.ReadByte();
        reader.EnsureFullyRead();
        return new RegisterOTPVerificationRequest { Email = email, OTPCode = code, Purpose = purpose };
    }

    public static byte[] CreateResult(Opcode opcode, OtpResult result)
    {
        EnsureResultOpcode(opcode);
        ArgumentNullException.ThrowIfNull(result);
        var writer = new PacketWriter();
        writer.WriteBoolean(result.Success);
        writer.WriteString(result.Message);
        return writer.Build(opcode);
    }

    public static OtpResult ReadResult(Packet packet, Opcode expected)
    {
        EnsureResultOpcode(expected);
        PacketGuard.EnsureOpcode(packet, expected);
        var reader = new PacketReader(packet.Payload);
        var result = new OtpResult { Success = reader.ReadBoolean(), Message = reader.ReadString(PacketConstants.MaximumMessageByteLength) };
        reader.EnsureFullyRead();
        return result;
    }

    private static void EnsureVerificationOpcode(Opcode opcode)
    {
        if (opcode is not (Opcode.C2S_RegisterOTPVerificationRequest or Opcode.C2S_ResetPassOTPVerificationRequest))
            throw new ProtocolException("Opcode không phải yêu cầu xác minh OTP.");
    }

    private static void EnsureResultOpcode(Opcode opcode)
    {
        if (opcode is not (Opcode.S2C_RegisterOTPResult or Opcode.S2C_RegisterOTPVerificationResult or Opcode.S2C_ResetPassOTPVerificationResult))
            throw new ProtocolException("Opcode không phải kết quả OTP.");
    }
}
