using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class Agent60 : IAgent
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public string Name { get; set; }

        public Agent60(int id, int money, string name)
        {
            Id = id;
            Money = money;
            Name = name;
        }
        public bool WantToSub(int startPrice, int jumpPrice)
        {
            return (startPrice + jumpPrice < Money);

        }
        public Raiser WantToRaise(int startprice, int jumpPrice)
        {

            if (IsWantToRaise(startprice, jumpPrice))
            {
                Random rand = new Random();
                int newraise = rand.Next(startprice + jumpPrice, Money);
                Console.WriteLine("Agent : " + Name + " Raised");
                Console.WriteLine("Money of Agent {0} : {1}", Name, Money);
                Console.WriteLine("raise {0} : {1}", Name, newraise);
                return new Raiser(this, newraise);
            }
            else
            {
                return new Raiser(this, 0);
            }
        }
        public bool IsWantToRaise(int startprice, int jumpPrice)
        {
            //Might be here another logic to subscribe to Auction -- This is the different between WantToSub
            return (startprice + jumpPrice < (int)Money*0.6);
        }
    }
}
