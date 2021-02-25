using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public interface IProtocolOption
    {
        ProtocolTypes Type { get; }
        byte[] GetValue();
        void Decode(BinaryReader br);
    }
}
