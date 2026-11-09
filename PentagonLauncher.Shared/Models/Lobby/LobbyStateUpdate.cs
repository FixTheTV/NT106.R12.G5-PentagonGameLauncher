using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Lobby
{
    public sealed class LobbyStateUpdate
    {
        public int LobbyId { get; init; }
        public int PlayerCount { get; init; }
        public byte PlayerSlot { get; init; }
        public bool IsReady { get; init; }
        public string Username { get; init; } = string.Empty;
    }
}
