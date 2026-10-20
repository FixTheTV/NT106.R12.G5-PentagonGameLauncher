using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PentagonLauncher.Shared.Models.Lobby
{
    public sealed class LobbyChatMessage
    {
        public string Username { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
    }
}
