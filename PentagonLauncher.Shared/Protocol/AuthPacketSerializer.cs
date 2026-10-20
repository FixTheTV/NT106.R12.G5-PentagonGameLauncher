using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using PentagonLauncher.Shared.Models.Authentication;
// chuyển model Authentication thành packet, phụ trách các Opcode 1001-1004, 2001-2004.
namespace PentagonLauncher.Shared.Protocol
{
    public static class AuthPacketSerializer
    {
        public static byte[] CreateRegisterPacket(RegisterRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            ValidateUsername(request.Username);
            ValidateEmail(request.Email);
            ValidatePassword(request.Password);

            PacketWriter writer = new();
            writer.WriteString(request.Username);
            writer.WriteString(request.Email);
            writer.WriteString(request.Password);

            return writer.Build(Opcode.C2S_RegisterRequest);
        }
        public static RegisterRequest ReadRegisterRequest(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_RegisterRequest);
            PacketReader reader = new(packet.Payload);
            string username = reader.ReadString(PacketConstants.MaximumUsernameByteLength);
            string email = reader.ReadString(PacketConstants.MaximunEmailByteLength);
            string password = reader.ReadString(PacketConstants.MaximumPasswordByteLength);

            reader.EnsureFullyRead();

            ValidateUsername(username);
            ValidateEmail(email);
            ValidatePassword(password);
            return new RegisterRequest
            {
                Username = username,
                Email = email,
                Password = password
            };

        }
        public static byte[] CreateRegisterResultPacker(RegisterResult result)
        {
            ArgumentNullException.ThrowIfNull(result);
            PacketWriter writer = new();
            writer.WriteBoolean(result.Success);
            writer.WriteString(result.Message);
            return writer.Build(Opcode.S2C_RegisterResult);
        }
        public static RegisterResult ReadRequestResult(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_RegisterResult);
            PacketReader reader = new(packet.Payload);
            bool success = reader.ReadBoolean();
            string message = reader.ReadString(PacketConstants.MaximumMessageByteLength);

            reader.EnsureFullyRead();

            return new RegisterResult
            {
                Success = success,
                Message = message
            };
        }

        public static byte[] CreareLoginRequest(LoginRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            ValidateUsername(request.Username);
            ValidatePassword(request.Password);
            PacketWriter writer = new();
            writer.WriteString(request.Username);
            writer.WriteString(request.Password);
            return writer.Build(Opcode.C2S_LoginRequest);

        }
        public static LoginRequest ReadLoginRequest(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_LoginRequest);
            PacketReader reader = new(packet.Payload);
            string username = reader.ReadString(PacketConstants.MaximumUsernameByteLength);
            string password = reader.ReadString(PacketConstants.MaximumPasswordByteLength);

            reader.EnsureFullyRead();
            ValidateUsername(username);
            ValidatePassword(password);
            return new LoginRequest
            {
                Username = username,
                Password = password
            };

        }
        public static byte[] CreateLoginResultPacket(LoginResult result)
        {
            ArgumentNullException.ThrowIfNull(result);
            PacketWriter writer = new();
            // thu tu phai dung theo : success, message, userId, username
            writer.WriteBoolean(result.Success);
            writer.WriteString(result.Message);
            writer.WriteInt32(result.UserId);
            writer.WriteString(result.Username);

            return writer.Build(Opcode.S2C_LoginResult);
        }

        public static LoginResult ReadLoginResult(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_LoginResult);
            PacketReader reader = new(packet.Payload);
            bool success = reader.ReadBoolean();
            string message = reader.ReadString(PacketConstants.MaximumMessageByteLength);
            int userid = reader.ReadInt32();
            string username = reader.ReadString(PacketConstants.MaximumUsernameByteLength);

            reader.EnsureFullyRead();
            return new LoginResult
            {
                Success = success,
                Message = message,
                UserId = userid,
                Username = username
            };
        }


        public static byte[] CreateForgotPasswordPacket(
        ForgotPasswordRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            ValidateEmail(request.Email);

            PacketWriter writer = new();

            writer.WriteString(request.Email);

            return writer.Build(Opcode.C2S_ForgotPasswordRequest);
        }

        public static ForgotPasswordRequest ReadForgotPasswordRequest(
            Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_ForgotPasswordRequest);

            PacketReader reader = new(packet.Payload);

            string email = reader.ReadString(
                PacketConstants.MaximunEmailByteLength);

            reader.EnsureFullyRead();

            ValidateEmail(email);

            return new ForgotPasswordRequest
            {
                Email = email
            };
        }

        public static byte[] CreateForgotPasswordResultPacket(
            ForgotPasswordResult result)
        {
            ArgumentNullException.ThrowIfNull(result);

            PacketWriter writer = new();

            writer.WriteBoolean(result.Success);
            writer.WriteString(result.Message);

            return writer.Build(Opcode.S2C_ForgotPasswordResult);
        }

        public static ForgotPasswordResult ReadForgotPasswordResult(
            Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_ForgotPasswordResult);

            PacketReader reader = new(packet.Payload);

            bool success = reader.ReadBoolean();

            string message = reader.ReadString(
                PacketConstants.MaximumMessageByteLength);

            reader.EnsureFullyRead();

            return new ForgotPasswordResult
            {
                Success = success,
                Message = message
            };
        }

        public static byte[] CreateResetPasswordPacket(
            ResetPasswordRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            ValidateEmail(request.Email);
            ValidateOtpCode(request.OtpCode);
            ValidatePassword(request.NewPassword);

            PacketWriter writer = new();

            writer.WriteString(request.Email);
            writer.WriteString(request.OtpCode);
            writer.WriteString(request.NewPassword);

            return writer.Build(Opcode.C2S_ResetPasswordRequest);
        }

        public static ResetPasswordRequest ReadResetPasswordRequest(
            Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_ResetPasswordRequest);

            PacketReader reader = new(packet.Payload);

            string email = reader.ReadString(
                PacketConstants.MaximunEmailByteLength);

            string otpCode = reader.ReadString(
                PacketConstants.MaximumOtpcodeByteLength);

            string newPassword = reader.ReadString(
                PacketConstants.MaximumPasswordByteLength);

            reader.EnsureFullyRead();

            ValidateEmail(email);
            ValidateOtpCode(otpCode);
            ValidatePassword(newPassword);

            return new ResetPasswordRequest
            {
                Email = email,
                OtpCode = otpCode,
                NewPassword = newPassword
            };
        }

        public static byte[] CreateResetPasswordResultPacket(
            ResetPasswordResult result)
        {
            ArgumentNullException.ThrowIfNull(result);

            PacketWriter writer = new();

            writer.WriteBoolean(result.Success);
            writer.WriteString(result.Message);

            return writer.Build(Opcode.S2C_ResetPasswordResult);
        }

        public static ResetPasswordResult ReadResetPasswordResult(
            Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_ResetPasswordResult);

            PacketReader reader = new(packet.Payload);

            bool success = reader.ReadBoolean();

            string message = reader.ReadString(
                PacketConstants.MaximumMessageByteLength);

            reader.EnsureFullyRead();

            return new ResetPasswordResult
            {
                Success = success,
                Message = message
            };
        }

        private static void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ProtocolException("Username không được để trống.");
            }
            if (username.Length < PacketConstants.MinimumUsernameLength || username.Length > PacketConstants.MaximumUsernameByteLength)
            {
                throw new ProtocolException($"username phai co tu {PacketConstants.MinimumUsernameLength} den {PacketConstants.MaximumUsernameByteLength} ki tu");
            }
        }
        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ProtocolException("Email khong duoc de trong");
            }
        }
        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ProtocolException("Password khong duoc de trong.");
            }
        }
        private static void ValidateOtpCode(string Otpcode)
        {
            if (Otpcode.Length != PacketConstants.OtpcodeLength)
            {
                throw new ProtocolException($"Opt phai co dung {PacketConstants.OtpcodeLength} ky tu.");
            }
            foreach (char character in Otpcode)
            {
                if (!char.IsDigit(character))
                {
                    throw new ProtocolException("OTP chi duoc chua chu so");
                }
            }
        }
    }
}
