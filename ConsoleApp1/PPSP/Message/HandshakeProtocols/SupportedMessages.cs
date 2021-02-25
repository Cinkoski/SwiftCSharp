using System;
using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class SupportedMessages : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.SupportedMessages;
        private byte _value;

        public byte[] GetValue()
        {
            Console.WriteLine("SupportedMessages not implemented!");
            throw new NotImplementedException();
        }

        public void Decode(BinaryReader br)
        {
            Console.WriteLine("SupportedMessages not implemented!");
            throw new NotImplementedException();
        }
    }
}
