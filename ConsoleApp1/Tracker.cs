using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Utf8Json;
using Utf8Json.Resolvers;

namespace ConsoleApp1
{
    public class Tracker
    {
        private const string _trackerUrl = "http://tracker.deltamediaplayer.com/announce?sessionDev=";
        private const string _getSessionUrl = "http://80.211.35.134/playercode/p2pengine/v2/getsession.php";
        private const int _defaultPeerCount = 30;

        private readonly IPEndPoint _remoteAddress;
        private readonly IPEndPoint _localAddress;
        private string _peerId;

        public Tracker(IPEndPoint remoteAddres, IPEndPoint localAddress)
        {
            _remoteAddress = remoteAddres;
            _localAddress = localAddress;
            _peerId = generatePeerId();
            JsonSerializer.SetDefaultResolver(StandardResolver.ExcludeNullSnakeCase);
        }

        public void Join(string swarmHash)
        {
            var protocolModel = buildBaseObject(swarmHash, SwarmAction.JOIN);
            var json = JsonSerializer.ToJsonString(protocolModel);

            var resultJson = sendPostRequest(json);
            var resultObject = JsonSerializer.Deserialize<SwiftProtocol>(resultJson);
        }

        public void Find(string swarmHash)
        {
            // why would i need it?
        }

        public void Leave(string swarmHash)
        {
            var protocolModel = buildBaseObject(swarmHash, SwarmAction.LEAVE);
            var json = JsonSerializer.ToJsonString(protocolModel);

            var resultJson = sendPostRequest(json);
            var resultObject = JsonSerializer.Deserialize<SwiftProtocol>(resultJson);
        }

        private string sendPostRequest(string jsonData)
        {
            var sessionId = getSessionId();
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = Helper.WebClient.PostAsync($"{_trackerUrl}{sessionId}", jsonContent).Result;

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(result.StatusCode.ToString());
            }

            return result.Content.ReadAsStringAsync().Result;
        }

        private string getSessionId()
        {
            var result = Helper.WebClient.GetAsync(_getSessionUrl).Result;
            return result.Content.ReadAsStringAsync().Result;
        }

        private SwiftProtocol buildBaseObject(string swarmHash, SwarmAction swarmAction)
        {
            return new SwiftProtocol
            {
                Container = new PPSPTrackerProtocol()
                {
                    Version = 1,
                    RequestType = RequestTypeEnum.CONNECT,
                    TransactionId = generateTransactionId(),
                    PeerId = _peerId,
                    Connect = new ConnectModel()
                    {
                        PeerNum = new PeerNumModel()
                        {
                            PeerCount = _defaultPeerCount,
                            ConcurrentLinks = ConcurrentLinksEnum.NORMAL,
                            OnlineTime = OnlineTimeEnum.NORMAL,
                            UploadBandwidthLevel = UploadBandwidthLevelEnum.NORMAL
                        },
                        PeerAddr = getPeeerAddrModel(_remoteAddress, _localAddress),
                        SwarmAction = new List<SwarmActionModel>()
                        {
                            new SwarmActionModel()
                            {
                                SwarmId = swarmHash,
                                Action = swarmAction,
                                PeerMode = PeerModeEnum.LEECH
                            }
                        }
                    }
                }
            };
        }

        private List<PeerAddrModel> getPeeerAddrModel(IPEndPoint remoteAddress, IPEndPoint localAddress)
        {
            return new List<PeerAddrModel>()
            {
                new PeerAddrModel()
                {
                    IpAddress = new IpAddressModel()
                    {
                        AddressType = AddressTypeEnum.Ipv4,
                        Address = remoteAddress.Address.ToString()
                    },
                    Port = remoteAddress.Port,
                    Priority = 1,
                    Type = PeerAddrType.REFLEXIVE
                },
                new PeerAddrModel()
                {
                    IpAddress = new IpAddressModel()
                    {
                        AddressType = AddressTypeEnum.Ipv4,
                        Address = localAddress.Address.ToString()
                    },
                    Port = localAddress.Port,
                    Priority = 2,
                    Type = PeerAddrType.HOST
                }
            };
        }

        private string generateTransactionId() => new Random().Next(0, 9999).ToString("D4");

        private string generatePeerId() => "-SW1000-" + Helper.RandomLong(0, 999999999999).ToString("D12");
    }
}
