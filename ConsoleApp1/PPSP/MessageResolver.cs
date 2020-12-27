using System;
using System.IO;
using System.Threading.Tasks;

namespace SwiftCSharp.PPSP
{
    public class MessageResolver
    {
        public static void Resolve(Task<byte[]> input, byte[] channelId)
        {
            byte[] resultBytes = input.Result;
            if (resultBytes == null)
                return;

            using (Stream ms = new MemoryStream(resultBytes))
            using (BinaryReader br = new BinaryReader(ms))
            {
                //var msgChannelId = br.ReadInt32();

                /*if (msgChannelId != channelId)
                {
                    Console.WriteLine("Channel Ids are not equal");
                    return;
                }*/

                //var msgType = br.ReadByte();
            }
        }
    }
}