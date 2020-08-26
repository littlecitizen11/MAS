using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace MAS
{
    public class Auction<T>
    {
        private static Timer aTimer;
        public int Id { get; set; }
        public T Product { get; set; }
        public int Price { get; set; }
        public int JumpOfPrice { get; set; }
        public DateTime StartDateAndTime { get; set; }
        public bool IsOpen { get; set; }
        public List<IAgent> Agents { get; set; }
        public event Func<int, int, bool> UpdateAuction;
        public Auction(int id, T product, int price, int jumpofprice, DateTime startDateTime)
        {
            Id = id;
            Product = product;
            Price = price;
            JumpOfPrice = jumpofprice;
            StartDateAndTime = startDateTime;
            IsOpen = false;
            Agents = new List<IAgent>();

        }
        public void StartAuction()
        {
            IsOpen = true;
        }
        public void RunAuction()
        {
            aTimer = new Timer();
            aTimer.Interval = 10;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            bool cancel = false;
            aTimer.Elapsed += (s, e) => cancel = true;
            aTimer.Start();
            while (!cancel)
            {
                if (UpdateAuction.GetInvocationList().Length > 1)
                {
                    Update();
                    aTimer.Interval = 10;
                }
                else
                    cancel=true;
            }
            //IsOpen = false;
            Console.WriteLine("Finish");

        }   
        public void Subscribe(IAgent agent)
        {
            UpdateAuction += new Func<int, int, bool>(agent.WantToRaise);
        }
        public void UnSubscribe(IAgent agent)
        {
            foreach (var item in Agents)
            {
                if(!item.IsWantToRaise(Price,JumpOfPrice))
                {
                    UpdateAuction -= new Func<int, int, bool>(item.WantToRaise);
                    Agents.Remove(item);
                }
            }
        }

        public void Update()
        {
            UpdateAuction?.Invoke(Price, JumpOfPrice);
        }
    }
}
