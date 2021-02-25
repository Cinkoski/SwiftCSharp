using SwiftCSharp.PPSP.Message.Properties;
using SwiftCSharp.PPSP.Protocol;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwiftCSharp.PPSP.Message
{
    public class Handshake : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.HANDSHAKE;

        public ChannelId SrcChannelId { get; private set; } // TODO: only used for Handshake, come up with some new 
        private List<IProtocolOption> _options = new List<IProtocolOption>();

        public Handshake() { }

        public Handshake(ChannelId destChannelId)
        {
            DestChannelId = destChannelId;
        }

        public Handshake(string swarmId)
        {
            DestChannelId = new ChannelId();
            SrcChannelId = new ChannelId(true);

            _options.Add(new ProtocolVersion());
            _options.Add(new MinimumVersion());
            _options.Add(new SwarmIdentifier(swarmId));
            _options.Add(new CipMethod());
            _options.Add(new LiveSignatureAlgorithm());
            _options.Add(new ChunkAdressingMethod());
            _options.Add(new LiveDiscardWindow());
            _options.Add(new EndOption());
        }

        public override byte[] ToByteArray()
        {
            var outputBuffer = ToByteList();
            outputBuffer.AddRange(SrcChannelId.ToByteArray());

            foreach (var option in _options)
            {
                outputBuffer.Add((byte)option.Type);
                outputBuffer.AddRange(option.GetValue());
            }

            return outputBuffer.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            SrcChannelId = new ChannelId(br.ReadBytes(4));

            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                var protocolType = (ProtocolTypes)br.ReadByte();
                var protocolObject = ProtocolTypesHelper.GetObject(protocolType);
                protocolObject.Decode(br);

                _options.Add(protocolObject);

                if (protocolType == ProtocolTypes.EndOption)
                    break;
            }
        }

        public T GetOption<T>() where T : IProtocolOption => (T)_options.Single(o => o.GetType() == typeof(T));
    }
}