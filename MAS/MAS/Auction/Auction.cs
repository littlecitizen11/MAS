using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MAS
{
    public class Auction
    {
        private static Timer aTimer;
        public int Id { get; set; }
        public IItem Product { get; set; }
        public int Price { get; set; }
        public int JumpOfPrice { get; set; }
        public DateTime StartDateAndTime { get; set; }
        public bool IsOpen { get; set; }
        public List<IAgent> Agents { get; set; }
        public Raiser MaxRaiser { get; set; }
        public event Func<int, int, Raiser> UpdateAuction;

        public Auction(int id, IItem product, int price, int jumpofprice, DateTime startDateTime)
        {
            Id = id;
            Product = product;
            Price = price;
            JumpOfPrice = jumpofprice;
            StartDateAndTime = startDateTime;
            IsOpen = false;
            Agents = new List<IAgent>();
            MaxRaiser = new Raiser();

        }
        public void PrintAuctionStart()
        {
            Console.WriteLine("-----------------------");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Auction Opened!");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Auction of the item : " + Product.Name);
            Console.WriteLine("First Price : " + Price);
            Console.WriteLine("Jumps : " + JumpOfPrice);
            Console.WriteLine("-----------------------");
        }

        public void PrintAuctionEnd()
        {
            Console.WriteLine("-----------------------");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("Auction Ended!");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Auction of the item : " + Product.Name);
            if (MaxRaiser.Agent != null)
            {
                Console.WriteLine("The winner is : " + MaxRaiser.Agent.Name);
                Console.WriteLine("Price Win : " + Price);
            }
            else
                Console.WriteLine("No winners");
            Console.WriteLine("-----------------------");
        }

        public int RunAuction()
        {
            System.Threading.Thread.Sleep(1000);
            PrintAuctionStart();
            IsOpen = true;
            aTimer = new Timer(10000);
            bool cancel = false;
            aTimer.Elapsed += (s, e) => { cancel = true; aTimer.Stop(); aTimer.Enabled = false; };
            aTimer.Start();
            while (!cancel)
            {
                if (UpdateAuction != null)
                {
                    if (UpdateAuction.GetInvocationList().Length > 1)
                    {
                        ExecuteAuctionRound();
                        UnSubscribe();
                        Price = MaxRaiser.RaisePrice;
                        if (UpdateAuction != null)
                        {
                            if (UpdateAuction.GetInvocationList().Length > 1)
                            {
                                aTimer.Stop();
                                aTimer.Close(); 
                                //aTimer.Dispose();
                                //aTimer = new Timer(10000);
                                aTimer.Interval=1000;
                                aTimer.Start();
                            }
                            else
                            {
                                cancel = true;
                            }
                        }
                        else
                            cancel = true;
                    }
                }
                else
                    cancel = true;
            }
            aTimer.Stop();
            aTimer.Enabled = false;
            IsOpen = false;
            aTimer.Dispose();
            PrintAuctionEnd();
            if (MaxRaiser.Agent != null)
            {
                MaxRaiser.Agent.Money -= MaxRaiser.RaisePrice;
                return Price;
            }
            else
                return 0;
        }
        public void Subscribe(IAgent agent)
        {
            UpdateAuction += new Func<int, int, Raiser>(agent.WantToRaise);
        }
        public void UnSubscribe()
        {
            foreach (var item in Agents)
            {
                if (!item.IsWantToRaise(Price, JumpOfPrice))
                {
                    UpdateAuction -= new Func<int, int, Raiser>(item.WantToRaise);
                }
            }
        }
        public void ExecuteAuctionRound()
        {
            List<Task> tasks = new List<Task>();
            List<Raiser> raisers = new List<Raiser>();
            object _lock = new object();
            if (UpdateAuction != null)
            {
                Parallel.ForEach(UpdateAuction.GetInvocationList(), item =>
  {
      tasks.Add(Task.Factory.StartNew(() =>
      {
          lock (_lock)
              raisers.Add((Raiser)item.DynamicInvoke(Price, JumpOfPrice));
      }));
  });
            }
            Task.WaitAll(tasks.ToArray());
            if (raisers != null)
            {
                var check = raisers.OrderByDescending(item => item.RaisePrice).First();

                if (check.RaisePrice > Price)
                {
                    Price = check.RaisePrice;
                    MaxRaiser = check;
                }
            }

        }
    }
}
