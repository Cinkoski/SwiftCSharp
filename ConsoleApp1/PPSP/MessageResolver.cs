using SwiftCSharp.PPSP.Message;

namespace SwiftCSharp.PPSP
{
    public class MessageResolver
    {
        public AbstractMessage FromByteArray(byte[] input)
        {
            // first 4 bytes = address
            // 5 byte = message type
            // next 4 bytes = address
            // rest = message contents

            return null;
        }
    }
}
