﻿
using System.IO;

namespace SwiftCSharp.PPSP.Message
{
    public class Have : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.HAVE;

        public uint BinValue;

        public override byte[] ToByteArray()
        {
            var bytes = ToByteList();
            return bytes.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            BinValue = br.ReadUInt32BE();
        }
    }
}
