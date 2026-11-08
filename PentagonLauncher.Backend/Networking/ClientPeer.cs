using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using PentagonLauncher.Shared.Protocol;

namespace PentagonLauncher.Backend.Networking
{
    public enum ClientState
    {
        Connected,
        Authenticated,
        Disconnected
    }

    public class ClientPeer
    {
        public Guid ConnectionId { get; } = Guid.NewGuid();
        public TcpClient TcpClient { get; }
        public NetworkStream Stream => TcpClient.GetStream();

        // Thông tin phiên đăng nhập (Session)
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public ClientState State { get; set; } = ClientState.Connected;

        public ClientPeer(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
        }

        // Gửi mảng byte hoàn chỉnh được tạo từ PacketWriter.Build() của Minh Hiếu
        public async Task SendPacketAsync(byte[] packetBytes)
        {
            if (!TcpClient.Connected) return;

            await Stream.WriteAsync(packetBytes, 0, packetBytes.Length);
            await Stream.FlushAsync();
        }

        public void Disconnect()
        {
            State = ClientState.Disconnected;
            try
            {
                TcpClient.Close();
            }
            catch { /* Bỏ qua lỗi khi đóng socket */ }
        }
    }
}
