using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using CaroOnline.WinForms.Forms;
using CaroOnline.WinForms.Services;

namespace CaroOnline.WinForms;

internal static class Program
{
    // Đổi DemoAuthService thành adapter gọi API của nhóm Backend khi tích hợp.
    public static IAuthService Auth { get; } = new DemoAuthService();

    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }
}
