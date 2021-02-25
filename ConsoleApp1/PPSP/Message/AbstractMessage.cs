using SwiftCSharp.PPSP.Message.Properties;
using System.Collections.Generic;
using System.IO;

namespace SwiftCSharp.PPSP.Message
{
    public abstract class AbstractMessage
    {
        public abstract MessageTypes Type { get; }
        public abstract byte[] ToByteArray(); // TODO: Think of some way to not include ending message in every possible message type in ToByteArray method
        public abstract void Decode(BinaryReader br);
        public ChannelId DestChannelId { get; set; }

        public List<byte> ToByteList()
        {
            List<byte> outputBuffer = new List<byte>();

            if (DestChannelId != null)
                outputBuffer.AddRange(DestChannelId.ToByteArray());
            outputBuffer.Add((byte)Type);

            return outputBuffer;
        }

        public byte[] ToArray() // TODO: Change method name and/or scope
        {
            return ToByteList().ToArray();
        }
    }
}
