
using SwiftCSharp.PPSP.Message.Properties;
using System.IO;

namespace SwiftCSharp.PPSP.Message
{
    public class PexReq : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.PEX_REQ;

        public PexReq(ChannelId channelId)
        {
            DestChannelId = channelId;
        }

        public override byte[] ToByteArray()
        {
            var bytes = ToByteList();
            return bytes.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            throw new System.NotImplementedException();
        }
    }
}
