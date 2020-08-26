using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public event Func<int, int, int> UpdateAuction;

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
        public int RunAuction()
        {
            IsOpen = true;
            aTimer = new Timer();
            aTimer.Interval = 100;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
            bool cancel = false;
            aTimer.Elapsed += (s, e) => cancel = true;
            aTimer.Start();
            while (!cancel)
            {

                if (UpdateAuction.GetInvocationList().Length > 1)
                {
                    Price+=Update();
                    aTimer = new Timer();
                    aTimer.Interval = 100;
                    Console.WriteLine("######");
                    Console.WriteLine("Price now is : " + Price);
                    Console.WriteLine("######");
                }
                else
                    cancel = true;
            }
            aTimer.Enabled = false;
            aTimer.Stop();
            aTimer.Close();
            IsOpen = false;
            Console.WriteLine("Finish");
            return 10;
        }   
        public void Subscribe(IAgent agent)
        {
            UpdateAuction += new Func<int, int, int>(agent.WantToRaise);
        }
        public void UnSubscribe(IAgent agent)
        {
            foreach (var item in Agents)
            {
                if(!item.IsWantToRaise(Price,JumpOfPrice))
                {
                    UpdateAuction -= new Func<int, int, int>(item.WantToRaise);
                    //Agents.Remove(item);
                }
            }
        }

        public int Update()
        {
            //UpdateAuction?.Invoke(Price, JumpOfPrice);
            //UpdateAuction.GetInvocationList()
            var tasks = new List<Task>();
            var numbers = new List<int>();
            foreach (Func<int, int, int> item in UpdateAuction.GetInvocationList())
            {
                numbers.Add(item.Invoke(Price,JumpOfPrice));
            }
            return numbers.Max();
            
        }
    }
}
