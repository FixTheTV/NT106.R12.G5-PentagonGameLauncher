using System;
using System.Collections.Concurrent;

namespace PentagonLauncher.Backend.Networking
{
    public class SessionManager
    {
        private readonly object _sessionLock = new();
        // Tất cả các kết nối TCP vật lý đang mở
        public ConcurrentDictionary<Guid, ClientPeer> ConnectedPeers { get; } = new();

        // Danh sách tài khoản đang Online (UserId -> ClientPeer)
        public ConcurrentDictionary<int, ClientPeer> ActiveClients { get; } = new();

        public void AddPeer(ClientPeer peer)
        {
            ConnectedPeers.TryAdd(peer.ConnectionId, peer);
        }

        // Khi đăng nhập thành công (result.Success == true)
        public void AuthenticatePeer(ClientPeer peer, int userId, string username)
        {
            lock (_sessionLock)
            {
            if (peer.UserId.HasValue && peer.UserId.Value != userId) LogoutPeer(peer);
            // Ngắt kết nối phiên cũ nếu tài khoản này đang đăng nhập ở máy khác
            if (ActiveClients.TryRemove(userId, out var oldPeer))
            {
                if (!ReferenceEquals(oldPeer, peer)) oldPeer.Disconnect();
            }

            peer.UserId = userId;
            peer.Username = username;
            peer.State = ClientState.Authenticated;

            ActiveClients[userId] = peer;
            }
        }

        // Khi người dùng đăng xuất (Logout)
        public void LogoutPeer(ClientPeer peer)
        {
            lock (_sessionLock)
            {
            if (peer.UserId.HasValue)
            {
                // Remove only this connection; cleanup of an old peer must not remove its replacement.
                ((ICollection<KeyValuePair<int, ClientPeer>>)ActiveClients)
                    .Remove(new KeyValuePair<int, ClientPeer>(peer.UserId.Value, peer));
            }

            peer.UserId = null;
            peer.OtpPermissions.Clear();
            peer.Username = null;
            peer.State = ClientState.Connected;
            }
        }

        // Khi người dùng mất kết nối hoàn toàn
        public void RemoveDisconnectedPeer(ClientPeer peer)
        {
            LogoutPeer(peer);
            ConnectedPeers.TryRemove(peer.ConnectionId, out _);
            peer.Disconnect();
        }
    }
}
