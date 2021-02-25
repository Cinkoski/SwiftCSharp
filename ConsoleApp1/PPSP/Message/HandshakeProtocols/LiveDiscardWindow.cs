using System;
using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class LiveDiscardWindow : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.LiveDiscardWindow;
        private int _value = 0x400000;

        public byte[] GetValue()
        {
           return BitConverter.GetBytes(_value); // TBD: 32-bit or 64-bit
        }

        public int GetRawValue()
        {
            return _value;
        }

        public void Decode(BinaryReader br)
        {
            _value = br.ReadInt32();
        }
    }
}
