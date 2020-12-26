using System;

namespace SwiftCSharp.PPSP.Protocol
{
    public class Address : IProtocolOption
    {
        // TODO: Address reading and converting implementation

        private readonly string _address;

        public Address()
        {

        }

        public Address(string address)
        {
            _address = address;
        }

        public byte[] ToByteArray()
        {
            return new byte[4];
        }

        private byte[] getRandomBytes(int size)
        {
            var randomBytes = new byte[size];
            new Random().NextBytes(randomBytes);
            return randomBytes;
        }
    }
}
