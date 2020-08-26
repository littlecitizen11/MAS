using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IAgent
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public int WantToRaise(int startprice, int jumpPrice);
        public bool IsWantToRaise(int startprice, int jumpPrice);

    }
}
