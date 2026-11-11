using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Lobby
{
    public sealed class SetReadyStateRequest
    {
        public bool IsReady { get; init; }
    }
}
