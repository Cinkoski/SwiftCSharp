using SwiftCSharp.PPSP.Protocol;
using System.Collections.Generic;

namespace SwiftCSharp.PPSP.Message
{
    public abstract class AbstractMessage
    {
        public List<IProtocolOption> Options;

        public AbstractMessage()
        {
            Options = new List<IProtocolOption>();
        }

        public void FinishOptions()
        {
            Options.Add(new EndOption());
        }

        public byte[] ToByteArray()
        {
            List<byte> outputBuffer = new List<byte>();

            foreach (var option in Options)
            {
                outputBuffer.AddRange(option.ToByteArray());
            }

            return outputBuffer.ToArray();
        }
    }
}
