using STUN;
using STUN.Attributes;
using System;
using System.Net;

namespace SwiftCSharp
{
    public class StunClient
    {
        public STUNQueryResult GetAddress()
        {
            var stunServer = "stun.deltamediaplayer.com:3478";

            if (!STUNUtils.TryParseHostAndPort(stunServer, out IPEndPoint stunEndPoint))
                throw new Exception("Failed to resolve STUN server address");

            STUNClient.ReceiveTimeout = 500;
            var queryResult = STUNClient.Query(stunEndPoint, STUNQueryType.PublicIP, true);

            if (queryResult.QueryError != STUNQueryError.Success)
                throw new Exception("Query Error: " + queryResult.QueryError.ToString());

            Console.WriteLine("STUN: PublicEndPoint: {0}", queryResult.PublicEndPoint);
            Console.WriteLine("STUN: LocalEndPoint: {0}", queryResult.LocalEndPoint);

            return queryResult;
        }
    }
}
