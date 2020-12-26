using SwiftCSharp.PPSP.Protocol;

namespace SwiftCSharp.PPSP.Message
{
    public class Handshake : AbstractMessage
    {
        public Handshake(string swarmId)
        {
            MsgType = MessageType.HANDSHAKE;

            PrepareBaseOptions();

            Options.Add(new SwarmIdentifier(swarmId));
            Options.Add(new CipMethod());
            Options.Add(new LiveSignatureAlgorithm());
            Options.Add(new ChunkAdressingMethod());
            Options.Add(new LiveDiscardWindow());

            FinishOptions();
        }
    }
}