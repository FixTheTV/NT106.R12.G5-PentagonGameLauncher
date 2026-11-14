using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PentagonLauncher.Backend.Networking;
using PentagonLauncher.Backend.Services;
namespace PentagonLauncher.Backend
{
    internal class Program
    {
        private const int Port = 12345; // Cổng TCP theo tài liệu 

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"[*] BACKEND SERVER KHỞI ĐỘNG TRÊN CỔNG: {Port}");
            Console.WriteLine("_________________________________");

            // Khởi tạo các Service bên Long
            var databaseService = new DatabaseService();
            var authService = new AuthService(databaseService);

            // Khởi tạo quản lý phiên và bộ xử lý gói tin bên Khôi
            var sessionManager = new SessionManager();
            var tcpServer = new LauncherTcpServer(sessionManager, authService);

            // Khôi khởi động TCP Listener lắng nghe kết nối
            TcpListener server = new TcpListener(IPAddress.Any, Port);
            server.Start();
            Console.WriteLine("[*] Đang chờ client WinForms kết nối tới...");

            // Vòng lặp nhận client liên tục (của Khôi)
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine($"[+] Có client mới kết nối từ: {client.Client.RemoteEndPoint}");

                // Khôi bọc client này thành một ClientPeer và xử lý trong luồng riêng
                var peer = new ClientPeer(client);
                sessionManager.AddPeer(peer);
                _ = Task.Run(() => tcpServer.ReceiveLoopAsync(peer, CancellationToken.None));
            }
        }
    }
}
