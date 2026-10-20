using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Lobby
{
    public sealed class LobbyChatRequest
    {
        public string Message { get; init; } = string.Empty;
    }
}
