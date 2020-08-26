using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IAgent
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public string Name { get; set; }
        public Raiser WantToRaise(int startprice, int jumpPrice);
        public bool IsWantToRaise(int startprice, int jumpPrice);
        public bool WantToSub(int startPrice, int jumpPrice);

    }
}
