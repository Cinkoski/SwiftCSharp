using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ConsoleApp1
{
    public class Helper
    {
        public static HttpClient WebClient = new HttpClient();
        private static Random _random = new Random();

        public static string SwarmToHash(string swarmId)
        {
            var swarmBytes = StringToByteArray(swarmId);
            return GetHashSHA1(swarmBytes);
        }

        public static byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : 87);
        }

        public static string GetHashSHA1(byte[] data)
        {
            using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
            {
                return string.Concat(sha1.ComputeHash(data).Select(x => x.ToString("x2")));
            }
        }

        public static long RandomLong(long min, long max)
        {
            byte[] buf = new byte[8];
            _random.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        public static string ByteArrayToString(byte[] input, bool hexValues = true)
        {
            var sb = new StringBuilder("{ ");
            foreach (var b in input)
            {
                sb.Append(b.ToString("x2") + ", ");
            }
            sb.Append("}");
            return sb.ToString();
        }

        public static byte[] GetRandomBytes(int size)
        {
            var randomBytes = new byte[size];
            _random.NextBytes(randomBytes);
            return randomBytes;
        }
    }
}
