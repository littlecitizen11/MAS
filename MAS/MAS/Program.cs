using System;
using System.Collections.Generic;

namespace MAS
{
    class Program
    {
        static void Main(string[] args)
        {
            AuctionHandler ac = new AuctionHandler();
            ac.RunAuctions();
            Console.ReadLine();
        }
    }
}
