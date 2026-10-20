using System;
using System.Collections.Generic;
using System.Text;
// Đọc packet từ TCP, Xử lý việc TCP gửi packet thành nhiều đoạn.
namespace PentagonLauncher.Shared.Protocol
{
    public static class PacketStreamReader
    {
        public static async Task<Packet> ReadPacketAsync(
            Stream stream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(stream);
            byte[] headerBytes = new byte[PacketConstants.HeaderSize];

            await ReadExactlyAsync(stream, headerBytes, cancellationToken);
            PacketHeader header = PacketHeader.Parse(headerBytes);
            byte[] payload = new byte[header.PayloadLength];
            if (payload.Length > 0)
            {
                await ReadExactlyAsync(stream, headerBytes, cancellationToken);
            }
            return new Packet(header, payload);
        }


        public static async Task ReadExactlyAsync(Stream stream, Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            int totalRead = 0;
            while (totalRead < buffer.Length)
            {

                int read = await stream.ReadAsync(buffer[totalRead..], cancellationToken);
                if (read == 0)
                {
                    throw new EndOfStreamException("Remote endpoint đã đóng kết nối khi packet chưa được đọc xong .");
                }
                totalRead += read;
            }
        }
    }
}
