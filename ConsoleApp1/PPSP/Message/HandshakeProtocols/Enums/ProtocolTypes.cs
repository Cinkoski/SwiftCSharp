namespace SwiftCSharp.PPSP.Protocol
{
    public enum ProtocolTypes : byte
    {
        Version,
        MinimumVersion,
        SwarmIdentifier,
        CipMethod, //Content Integrity Protection
        MhtFunction, //Merkle Hash Tree Function
        LiveSignatureAlgorithm,
        ChunkAdressingMethod,
        LiveDiscardWindow,
        SupportedMessages,
        ChunkSize,
        UnassignedStart = 10,
        Unassigned,
        UnassignedEnd = 254,
        EndOption = 255
    }

    public class ProtocolTypesHelper
    {
        public static IProtocolOption GetObject(ProtocolTypes type)
        {
            switch (type)
            {
                case ProtocolTypes.Version:
                    return new ProtocolVersion();
                case ProtocolTypes.MinimumVersion:
                    return new MinimumVersion();
                case ProtocolTypes.SwarmIdentifier:
                    return new SwarmIdentifier();
                case ProtocolTypes.CipMethod:
                    return new CipMethod();
                case ProtocolTypes.MhtFunction:
                    return new MhtFunction();
                case ProtocolTypes.LiveSignatureAlgorithm:
                    return new LiveSignatureAlgorithm();
                case ProtocolTypes.ChunkAdressingMethod:
                    return new ChunkAdressingMethod();
                case ProtocolTypes.LiveDiscardWindow:
                    return new LiveDiscardWindow();
                case ProtocolTypes.SupportedMessages:
                    return new SupportedMessages();
                case ProtocolTypes.ChunkSize:
                    return new ChunkSize();
                case ProtocolTypes.EndOption:
                    return new EndOption();
                default:
                    return null;
            }
        }
    }
}
