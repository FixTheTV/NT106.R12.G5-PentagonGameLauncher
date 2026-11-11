using PentagonLauncher.Shared.Models.Lobby;
using System;
using System.Collections.Generic;
using System.Text;
// Chuyển các nodel Lobby thành packet, phụ trách Opcode 1101-1104, 2101-2103.
namespace PentagonLauncher.Shared.Protocol
{

    public static class LobbyPacketSerializer
    {
        public static byte[] CreateJoinLobbyRequestPacket(
            JoinLobbyRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            PacketWriter writer = new();

            writer.WriteInt32(request.LobbyId);

            return writer.Build(Opcode.C2S_JoinLobbyRequest);
        }

        public static JoinLobbyRequest ReadJoinLobbyRequest(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_JoinLobbyRequest);

            PacketReader reader = new(packet.Payload);

            int lobbyId = reader.ReadInt32();

            reader.EnsureFullyRead();

            return new JoinLobbyRequest
            {
                LobbyId = lobbyId
            };
        }

        public static byte[] CreateJoinLobbyResponsePacket(
            JoinLobbyResponse response)
        {
            ArgumentNullException.ThrowIfNull(response);

            PacketWriter writer = new();

            writer.WriteBoolean(response.Success);
            writer.WriteInt32(response.LobbyId);
            writer.WriteInt32(response.PlayerCount);
            writer.WriteString(response.Message);

            return writer.Build(Opcode.S2C_JoinLobbyResponse);
        }

        public static JoinLobbyResponse ReadJoinLobbyResponse(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_JoinLobbyResponse);

            PacketReader reader = new(packet.Payload);

            bool success = reader.ReadBoolean();
            int lobbyId = reader.ReadInt32();
            int playerCount = reader.ReadInt32();

            string message = reader.ReadString(
                PacketConstants.MaximumMessageByteLength);

            reader.EnsureFullyRead();

            return new JoinLobbyResponse
            {
                Success = success,
                LobbyId = lobbyId,
                PlayerCount = playerCount,
                Message = message
            };
        }

        public static byte[] CreateLeaveLobbyRequestPacket()
        {
            PacketWriter writer = new();

            return writer.Build(Opcode.C2S_LeaveLobbyRequest);
        }

        public static void ReadLeaveLobbyRequest(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_LeaveLobbyRequest);
            PacketGuard.EnsureEmptyPayload(packet);
        }

        public static byte[] CreateSetReadyStateRequestPacket(
            SetReadyStateRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            PacketWriter writer = new();

            writer.WriteBoolean(request.IsReady);

            return writer.Build(Opcode.C2S_SetReadyStateRequest);
        }

        public static SetReadyStateRequest ReadSetReadyStateRequest(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_SetReadyStateRequest);

            PacketReader reader = new(packet.Payload);

            bool isReady = reader.ReadBoolean();

            reader.EnsureFullyRead();

            return new SetReadyStateRequest
            {
                IsReady = isReady
            };
        }

        public static byte[] CreateLobbyChatRequestPacket(
            LobbyChatRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            PacketWriter writer = new();

            writer.WriteString(request.Message);

            return writer.Build(Opcode.C2S_LobbyChat);
        }

        public static LobbyChatRequest ReadLobbyChatRequest(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.C2S_LobbyChat);

            PacketReader reader = new(packet.Payload);

            string message = reader.ReadString(
                PacketConstants.MaximumChatByteLength);

            reader.EnsureFullyRead();

            return new LobbyChatRequest
            {
                Message = message
            };
        }

        public static byte[] CreateLobbyStateUpdatePacket(
            LobbyStateUpdate update)
        {
            ArgumentNullException.ThrowIfNull(update);

            PacketWriter writer = new();

            writer.WriteInt32(update.LobbyId);
            writer.WriteInt32(update.PlayerCount);
            writer.WriteByte(update.PlayerSlot);
            writer.WriteBoolean(update.IsReady);
            writer.WriteString(update.Username);

            return writer.Build(Opcode.S2C_LobbyStateUpdate);
        }

        public static LobbyStateUpdate ReadLobbyStateUpdate(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_LobbyStateUpdate);

            PacketReader reader = new(packet.Payload);

            int lobbyId = reader.ReadInt32();
            int playerCount = reader.ReadInt32();
            byte playerSlot = reader.ReadByte();
            bool isReady = reader.ReadBoolean();

            string username = reader.ReadString(
                PacketConstants.MaximumUsernameByteLength);

            reader.EnsureFullyRead();

            return new LobbyStateUpdate
            {
                LobbyId = lobbyId,
                PlayerCount = playerCount,
                PlayerSlot = playerSlot,
                IsReady = isReady,
                Username = username
            };
        }

        public static byte[] CreateLobbyChatPacket(
            LobbyChatMessage chatMessage)
        {
            ArgumentNullException.ThrowIfNull(chatMessage);

            PacketWriter writer = new();

            writer.WriteString(chatMessage.Username);
            writer.WriteString(chatMessage.Message);

            return writer.Build(Opcode.S2C_LobbyChat);
        }

        public static LobbyChatMessage ReadLobbyChat(Packet packet)
        {
            PacketGuard.EnsureOpcode(packet, Opcode.S2C_LobbyChat);

            PacketReader reader = new(packet.Payload);

            string username = reader.ReadString(
                PacketConstants.MaximumUsernameByteLength);

            string message = reader.ReadString(
                PacketConstants.MaximumChatByteLength);

            reader.EnsureFullyRead();

            return new LobbyChatMessage
            {
                Username = username,
                Message = message
            };
        }
    }
}
