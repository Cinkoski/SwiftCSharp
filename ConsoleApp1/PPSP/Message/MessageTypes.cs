namespace SwiftCSharp.PPSP.Message
{
    public enum MessageTypes
    {
        HANDSHAKE,
        DATA,
        ACK,
        HAVE,
        INTEGRITY,
        PEX_RES4,
        PEX_REQ,
        SIGNED_INTEGRITY,
        REQUEST,
        CANCEL,
        CHOKE,
        UNCHOKE,
        PEX_RESv6,
        PEX_REScert,
        UnassignedStart = 14,
        UnassignedEnd = 254,
        Reserved = 255
    }
}
