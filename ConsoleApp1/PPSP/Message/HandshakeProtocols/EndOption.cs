using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class EndOption : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.EndOption;

        public byte[] GetValue()
        {
            return new byte[0]; // Only type is used in message
        }

        public void Decode(BinaryReader br)
        {
            // Nothing to do here
        }
    }
}
