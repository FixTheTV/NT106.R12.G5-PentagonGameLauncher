using PentagonLauncher.Launcher.Forms;
using PentagonLauncher.Launcher.Services;

namespace PentagonLauncher.Launcher
{
    internal static class Program
    {
        // Temporary UI implementation; replace with the backend API adapter when available.
        public static IAuthService Auth { get; } = new DemoAuthService();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}
