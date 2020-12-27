using ConsoleApp1;
using System;

namespace SwiftCSharp.PPSP.Protocol
{
    public class ChannelId : IProtocolOption
    {
        // TODO: Channel reading and converting implementation

        private byte[] _channelBytes;

        public ChannelId(bool random = false)
        {
            if (random)
                _channelBytes = Helper.GetRandomBytes(4);
            else
                _channelBytes = new byte[] { 0, 0, 0, 0 };
        }

        public ChannelId(byte[] channelBytes)
        {
            _channelBytes = channelBytes;
        }

        public byte[] ToByteArray()
        {
            return _channelBytes;
        }

        public int ToInt()
        {
            return BitConverter.ToInt32(_channelBytes, 0);
        }
    }
}
