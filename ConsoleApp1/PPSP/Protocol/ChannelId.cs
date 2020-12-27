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
                _channelBytes = getRandomBytes(4);
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

        private byte[] getRandomBytes(int size)
        {
            var randomBytes = new byte[size];
            new Random().NextBytes(randomBytes);
            return randomBytes;
        }
    }
}
