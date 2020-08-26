using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class Raiser
    {
        public IAgent Agent { get; set; }
        public int RaisePrice { get; set; }
        public Raiser()
        { }
        public Raiser(IAgent agent, int raiseprice)
        {
            Agent = agent;
            RaisePrice = raiseprice;
        }
    }
}
