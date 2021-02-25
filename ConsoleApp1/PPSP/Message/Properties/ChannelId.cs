using System;

namespace SwiftCSharp.PPSP.Message.Properties
{
    public class ChannelId
    {
        protected byte[] ChannelBytes;

        public ChannelId(bool random = false)
        {
            if (random)
                ChannelBytes = Helper.GetRandomBytes(4);
            else
                ChannelBytes = new byte[] { 0, 0, 0, 0 };
        }

        public ChannelId(byte[] channelBytes)
        {
            ChannelBytes = channelBytes;
        }

        public byte[] ToByteArray()
        {
            return ChannelBytes;
        }

        public int ToInt()
        {
            return BitConverter.ToInt32(ChannelBytes, 0);
        }
    }
}
