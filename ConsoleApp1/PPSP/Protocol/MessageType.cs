using SwiftCSharp.PPSP.Message;

namespace SwiftCSharp.PPSP.Protocol
{
    public class MessageType : IProtocolOption
    {
        private readonly MessageTypes _msgType;

        public MessageType(MessageTypes msgType)
        {
            _msgType = msgType;
        }

        public byte[] ToByteArray()
        {
            return new byte[] { (byte)_msgType };
        }
    }
}
