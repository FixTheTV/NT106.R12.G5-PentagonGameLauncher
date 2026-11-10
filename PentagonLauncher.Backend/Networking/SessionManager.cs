using System;
using System.Collections.Concurrent;

namespace PentagonLauncher.Backend.Networking
{
    public class SessionManager
    {
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
            // Ngắt kết nối phiên cũ nếu tài khoản này đang đăng nhập ở máy khác
            if (ActiveClients.TryRemove(userId, out var oldPeer))
            {
                oldPeer.Disconnect();
            }

            peer.UserId = userId;
            peer.Username = username;
            peer.State = ClientState.Authenticated;

            ActiveClients[userId] = peer;
        }

        // Khi người dùng đăng xuất (Logout)
        public void LogoutPeer(ClientPeer peer)
        {
            if (peer.UserId.HasValue)
            {
                ActiveClients.TryRemove(peer.UserId.Value, out _);
            }

            peer.UserId = null;
            peer.Username = null;
            peer.State = ClientState.Connected;
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
