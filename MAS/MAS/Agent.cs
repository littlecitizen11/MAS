using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class Agent:IAgent
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public string Name { get; set; }

        public Agent(int id, int money, string name)
        {
            Id = id;
            Money = money;
            Name = name;
        }
        public bool WantToSub(int startPrice, int jumpPrice)
        {
            /*            Random _rnd = new Random();
                        int number = _rnd.Next(0, 3);
                        if (number == 0) return true;
                        else return false;*/
            if (startPrice + jumpPrice < Money)
                return true;
            else return false;
        }
        public Raiser WantToRaise(int startprice, int jumpPrice)
        {
            Random rand = new Random();
            int newraise = rand.Next(jumpPrice, Money);
            if (startprice+jumpPrice < Money)
            {
                Console.WriteLine("Agent : "+Id+" Raised");
                Console.WriteLine("Money of Agent {0} : {1}",Id,Money);
                Money -= newraise;
                Console.WriteLine("Money After raise of Agent {0} : {1}", Id, Money);
                return new Raiser(this, newraise);
            }
            else 
            {
                return new Raiser(this,0);
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
