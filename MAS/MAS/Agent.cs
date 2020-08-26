using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class Agent:IAgent
    {
        public int Id { get; set; }
        public int Money { get; set; }
        //public event Action NewAuction;
  
        public Agent(int id, int money)
        {
            Id = id;
            Money = money;
        }
        public bool WantToSub(int startPrice, int jumpPrice)
        {
            /*Random _rnd = new Random();
            int number = _rnd.Next(0, 1);
            if (number == 0) return true;
            else return false;*/
            return true;
        }
        public int WantToRaise(int startprice, int jumpPrice)
        {
            if (startprice + jumpPrice < Money)
            {
                Console.WriteLine("Agent : "+Id+" Raised");
                Money -= jumpPrice;
                Console.WriteLine("Money : "+Money);
                return jumpPrice;

            }
            else 
            {
                return 0;
            }
        }
        public bool IsWantToRaise(int startprice, int jumpPrice)
        {
            if (startprice + jumpPrice < Money)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
