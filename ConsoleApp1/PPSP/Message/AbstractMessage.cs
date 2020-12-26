using SwiftCSharp.PPSP.Protocol;
using System.Collections.Generic;

namespace SwiftCSharp.PPSP.Message
{
    public abstract class AbstractMessage
    {
        public List<IProtocolOption> Options;
        public MessageType MsgType;

        public AbstractMessage()
        {
            Options = new List<IProtocolOption>();
        }

        public void PrepareBaseOptions()
        {
            Options.Add(new Address() /*new byte[] { 0, 0, 0, 0 }*/); // TODO: use Address class, add dst 0 address
            Options.Add(new Address() /*getRandomBytes(4)*/); // TODO: use Address class, add src random address ??
        }

        public void FinishOptions()
        {
            Options.Add(new EndOption());
        }

        public byte[] ToByteArray()
        {
            List<byte> outputBuffer = new List<byte>();

            outputBuffer.Add((byte)MsgType);

            foreach (var option in Options)
            {
                outputBuffer.AddRange(option.ToByteArray());
            }

            return outputBuffer.ToArray();
        }
    }
}
