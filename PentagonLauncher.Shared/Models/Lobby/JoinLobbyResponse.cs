using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Lobby
{
    public sealed class JoinLobbyResponse
    {
        public bool Success { get; init; }
        public int LobbyId { get; init; }
        public int PlayerCount { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}
