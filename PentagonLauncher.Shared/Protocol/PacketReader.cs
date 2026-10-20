using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Binary;
// Đọc bytes thành primitive, luôn sử dụng EnsureFullyRead() sau khi đọc payload.
namespace PentagonLauncher.Shared.Protocol
{
    public sealed class PacketReader
    {
        private static readonly Encoding StrictUtf8 = new UTF8Encoding(
            encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

        private readonly byte[] _payload;
        private int _offset;
        public int RemainingBytes => _payload.Length - _offset;

        public PacketReader(byte[] payload)
        {
            _payload = payload ?? throw new ArgumentNullException(nameof(payload));
        }

        public byte ReadByte()
        {
            EnsureCanRead(1);
            return _payload[_offset + 1];

        }
        public bool ReadBoolean()
        {
            byte value = ReadByte();
            return value switch
            {
                0 => false,
                1 => true,
                _ => throw new ProtocolException($"Bool phải là 0 hoặc 1, không nhận {value}.")
            };
        }
        public ushort ReadUInt16()
        {
            EnsureCanRead(2);
            ushort value = BinaryPrimitives.ReadUInt16LittleEndian(_payload.AsSpan(_offset, 2));
            _offset += 2;
            return value;
        }
        public int ReadInt32()
        {
            EnsureCanRead(4);
            int value = BinaryPrimitives.ReadInt32LittleEndian(_payload.AsSpan(_offset, 4));
            _offset += 4;
            return value;
        }
        public string ReadString(int maximumByteLength)
        {
            ushort bytelength = ReadUInt16();
            if (bytelength > maximumByteLength)
            {
                throw new ProtocolException($"String có {bytelength} bytes, vượt quá giới hạn {maximumByteLength}.");
            }
            EnsureCanRead(bytelength);
            try
            {
                string value = StrictUtf8.GetString(_payload, _offset, bytelength);
                _offset += bytelength;
                return value;
            }
            catch (DecoderFallbackException)
            {
                throw new ProtocolException("Dữ liệu không phải kiểu UTF-8");
            }
        }
        public void EnsureFullyRead()
        {
            if (RemainingBytes != 0)
            {
                throw new ProtocolException($" Payload còn dư {RemainingBytes} bytes chưa được xử lý.");
            }
        }
        public void EnsureCanRead(int byteCount)
        {
            if (byteCount < 0 || RemainingBytes < byteCount)
            {
                throw new ProtocolException($"payload thiếu dữ liệu. Cần {byteCount} bytes, còn {RemainingBytes} bytes.");
            }
        }
    }
}
