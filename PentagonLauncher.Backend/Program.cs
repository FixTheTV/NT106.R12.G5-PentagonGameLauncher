using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PentagonLauncher.Backend.Networking;
using PentagonLauncher.Backend.Services;
using PentagonLauncher.Backend.Configuration;
using PentagonLauncher.Shared.Configuration;
namespace PentagonLauncher.Backend
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            EnvironmentFile.Load();
            int port = EnvironmentSettings.GetPort("SERVER_PORT", 12345);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"[*] BACKEND SERVER KHỞI ĐỘNG TRÊN CỔNG: {port}");
            Console.WriteLine("_________________________________");

            // Khởi tạo các Service bên Long
            var databaseService = new DatabaseService(EnvironmentSettings.GetDatabaseConnectionString());
            var authService = new AuthService(databaseService);

            // Khởi tạo quản lý phiên và bộ xử lý gói tin bên Khôi
            var sessionManager = new SessionManager();
            var tcpServer = new LauncherTcpServer(sessionManager, authService);

            // Khôi khởi động TCP Listener lắng nghe kết nối
            TcpListener server = new TcpListener(IPAddress.Any, port);
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
