using System;
using System.Collections.Generic;
using System.Text;
// Cố định lỗi, dùng cho việc hiển thị các lỗi trong quá trình làm việc .
namespace PentagonLauncher.Shared.Protocol
{
    public sealed class ProtocolException : Exception
    {
        public ProtocolException(string Message) : base(Message)
        {

        }
    }
}
