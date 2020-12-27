using SwiftCSharp.PPSP.Protocol;

namespace SwiftCSharp.PPSP.Message
{
    public class Handshake : AbstractMessage
    {
        public Handshake(string swarmId)
        {
            Options.Add(new ChannelId()); // DOC Section 8.4
            Options.Add(new MessageType(MessageTypes.HANDSHAKE));
            Options.Add(new ChannelId(true)); // The Source Channel ID: A locally unused channel ID
            Options.Add(new Version());
            Options.Add(new MinimumVersion());
            Options.Add(new SwarmIdentifier(swarmId));
            Options.Add(new CipMethod());
            Options.Add(new LiveSignatureAlgorithm());
            Options.Add(new ChunkAdressingMethod());
            Options.Add(new LiveDiscardWindow());

            FinishOptions();
        }
    }
}