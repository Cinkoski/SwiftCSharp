using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SwiftCSharp
{
    public class SwiftProtocol
    {
        [DataMember(Name = "PPSPTrackerProtocol")]
        public PPSPTrackerProtocol Container;
    }

    public class PPSPTrackerProtocol
    {
        public int Version;
        public RequestTypeEnum RequestType;
        public int ResponseType;
        public int ErrorCode;
        public string TransactionId;
        public string PeerAddr;
        public string PeerId;
        public ConnectModel Connect;
        public List<SwarmResultModel> SwarmResult;
    }

    public class ConnectModel
    {
        public PeerNumModel PeerNum;
        public List<PeerAddrModel> PeerAddr;
        public List<SwarmActionModel> SwarmAction;
    }

    public class SwarmResultModel
    {
        public string SwarmId;
        public int Result;
        public PeerGroupModel PeerGroup;
    }

    public class PeerGroupModel
    {
        public List<PeerInfoModel> PeerInfo;
    }

    public class PeerNumModel
    {
        public int PeerCount;
        public ConcurrentLinksEnum ConcurrentLinks;
        public OnlineTimeEnum OnlineTime;
        public UploadBandwidthLevelEnum UploadBandwidthLevel;
    }

    public class PeerInfoModel
    {
        public string PeerId;
        public PeerAddrModel PeerAddr;
    }

    public class PeerAddrModel
    {
        public IpAddressModel IpAddress;
        public int Port;
        public int Priority;
        public PeerAddrType Type;
        public string Connection;
        public string Asn;
        public string PeerProtocol;
    }

    public class SwarmActionModel
    {
        public string SwarmId;
        public SwarmAction Action;
        public PeerModeEnum PeerMode;
    }

    public class IpAddressModel
    {
        public AddressTypeEnum AddressType;
        public string Address;
    }

    public enum RequestTypeEnum
    {
        CONNECT
    }

    public enum ConcurrentLinksEnum
    {
        NORMAL
    }

    public enum OnlineTimeEnum
    {
        NORMAL
    }

    public enum UploadBandwidthLevelEnum
    {
        NORMAL
    }

    public enum AddressTypeEnum
    {
        ipv4, // small case when receiving
        Ipv4, // big case when sending
        ipv6,
        Ipv6
    }

    public enum PeerAddrType
    {
        REFLEXIVE,
        HOST
    }

    public enum SwarmAction
    {
        JOIN,
        FIND,
        LEAVE
    }

    public enum PeerModeEnum
    {
        LEECH
    }
}
