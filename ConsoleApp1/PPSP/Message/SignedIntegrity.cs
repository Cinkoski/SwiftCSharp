using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCSharp.PPSP.Message
{
    public class SignedIntegrity : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.INTEGRITY;

        public uint BinValue;
        public ulong Timestamp;
        public byte[] Signature;

        public override byte[] ToByteArray()
        {
            var bytes = ToByteList();
            return bytes.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            BinValue = br.ReadUInt32BE();
            Timestamp = br.ReadUInt64BE();
            Signature = br.ReadBytes(64);
        }
    }
}
