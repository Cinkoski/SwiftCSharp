using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var swiftClient = new Swift();
            swiftClient.Init();
            swiftClient.Connect("0d473b30fcbbded914e8d4cc0cc18bd7c2a5639dac04ce2137b66d4b6322c516e9c090cc80cebb611a8ff30e99a53e40610f5140fa58fd3cd840e3331d8d78b921");

            Console.ReadLine();
        }
    }
}
