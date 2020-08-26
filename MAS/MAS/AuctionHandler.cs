using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MAS
{
    public class AuctionHandler
    {
        public List<Agent> Agents { get; set; }
        public List<IBuilding> Buildings { get; set; }
        public int TotalPrice { get; set; }
        public List<Auction<IBuilding>> BuildingAuctions { get; set; }
        //public List<Auction<IBuilding>> BuildingsAuctions { get; set; }
        public AuctionHandler()
        {
            TotalPrice = 0;
            Agents = new List<Agent>();
            Agents.Add(new Agent(1, 5));
            Agents.Add(new Agent(2, 3));
            Agents.Add(new Agent(3, 2));
            Buildings = new List<IBuilding>();
            Buildings.Add(new Office("Feldman", 2, 10, true, false, 2));
            Buildings.Add(new Resident("Carmel", 2, 10, true, false, 2));
            BuildingAuctions = new List<Auction<IBuilding>>();
            BuildingAuctions.Add(new Auction<IBuilding>(1, Buildings[0], 1, 1, DateTime.Now.AddSeconds(5)));
        }
        public void Checker()
        {
            var tasks = new List<Task>();


                foreach (var item in BuildingAuctions)
                {
                    System.Timers.Timer timer;
                    DateTime nowTime = DateTime.Now;
                    int tickTime = (int)(item.StartDateAndTime - DateTime.Now).TotalMilliseconds;

                    tasks.Add(Task.Factory.StartNew(
                        () => {
                            ExecuteAuction(item, tickTime);
                        } ));
                    
                    /*timer = new Timer(tickTime);
                    timer.AutoReset = false;
                    timer.Enabled = true;
                    //object _lock = new object();
                    timer.Elapsed += (s, e) => { if (timer.Enabled) { item.RunAuction(); timer.Enabled = false; s.Stop();}  };

                    foreach (var agent in Agents)
                    {
                        if (agent.WantToSub(item.Price, item.JumpOfPrice))
                        {
                            item.Agents.Add(agent);
                            item.Subscribe(agent);
                        }
                    }
                    timer.Start();*/
                }

            
        }
        public async Task<int> ExecuteAuction(Auction<IBuilding> auction, int milisec)
        {
            await Task.Delay(milisec);
            foreach (var agent in Agents)
            {
                if (agent.WantToSub(auction.Price, auction.JumpOfPrice))
                {
                    auction.Agents.Add(agent);
                    auction.Subscribe(agent);
                }
            }
            return auction.RunAuction();

        }

    }

}
