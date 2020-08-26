using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MAS
{
    public class AuctionHandler
    {
        public List<IAgent> Agents { get; set; }
        public List<IItem> Buildings { get; set; }
        public ConcurrentDictionary<int,int> TotalPrice { get; set; }
        public List<Auction> BuildingAuctions { get; set; }
        public AuctionHandler()
        {
            TotalPrice = new ConcurrentDictionary<int, int>();
            Agents = new AgentFactory().AgentList;
            Buildings = new ItemFactory().GetBuildingLists();
            BuildingAuctions = new List<Auction>();     
            BuildingAuctions.Add(new Auction(1, Buildings[0], 1, 1, DateTime.Now.AddSeconds(5)));
            BuildingAuctions.Add(new Auction(2, Buildings[1], 10, 2, DateTime.Now.AddSeconds(6)));

        }
        public void RunAuctions()
        {
            List<Task> tasks = new List<Task>();
                foreach (var item in BuildingAuctions)
                {
                    DateTime nowTime = DateTime.Now;
                    int tickTime = (int)(item.StartDateAndTime - DateTime.Now).TotalMilliseconds;
                    tasks.Add(Task.Run(async
                        () => {
                             await AsyncExecuteAuction(item, tickTime);
                        } ));
                }
            Task.WaitAll(tasks.ToArray());
            Task.WaitAny(tasks.ToArray());
        }
        public async Task AsyncExecuteAuction(Auction auction, int milisec)
        {
            await Task.Delay(milisec);
            Parallel.ForEach(Agents, agent => {
                if (agent.WantToSub(auction.Price, auction.JumpOfPrice))
                {
                    auction.Agents.Add(agent);
                    auction.Subscribe(agent);
                }
            });
            TotalPrice.TryAdd(auction.Id, auction.RunAuction());
            Console.WriteLine("Total sum is : " + TotalPrice.Values.Sum());

        }

    }

}
