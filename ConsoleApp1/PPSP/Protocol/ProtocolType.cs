namespace SwiftCSharp.PPSP.Protocol
{
    public enum ProtocolType
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
        UnassignedEnd = 254,
        EndOption = 255
    }
}
