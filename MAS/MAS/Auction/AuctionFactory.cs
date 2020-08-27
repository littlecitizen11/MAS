using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class AuctionFactory
    {
        public List<Auction> AuctionsList { get; set; }
        public List<IItem> ItemLists { get; set; }
        public AuctionFactory()
        {
            ItemLists = new ItemFactory().ItemLists;
            AuctionsList = new List<Auction>();
            AuctionsList.Add(new Auction(1, ItemLists[0], 1, 3, DateTime.Now.AddSeconds(5)));
            AuctionsList.Add(new Auction(2, ItemLists[1], 10, 2, DateTime.Now.AddSeconds(6)));
        }
    }
}
