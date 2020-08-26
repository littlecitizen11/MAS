using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAS
{
    public class AuctionHandler
    {
        public List<Agent> Agents { get; set; }
        public List<IBuilding> Buildings { get; set; }
        public List<Auction<IBuilding>> BuildingAuctions { get; set; }
        //public List<Auction<IBuilding>> BuildingsAuctions { get; set; }
        public AuctionHandler()
        {
            Agents = new List<Agent>();
            Agents.Add(new Agent(1, 5));
            Agents.Add(new Agent(2, 3));
            Agents.Add(new Agent(3, 2));
            Buildings = new List<IBuilding>();
            Buildings.Add(new Office("Feldman", 2, 10, true, false, 2));
            Buildings.Add(new Resident("Carmel", 2, 10, true, false, 2));
            BuildingAuctions = new List<Auction<IBuilding>>();
            BuildingAuctions.Add(new Auction<IBuilding>(1, Buildings[0], 1, 1, DateTime.Now.AddSeconds(10)));
        }
        public void Checker()
        {
            while (BuildingAuctions.Where(x => x.StartDateAndTime.Second >= DateTime.Now.Second).Count() > 0)
            {
                foreach (var item in BuildingAuctions)
                {
                    if (item.StartDateAndTime.Minute == DateTime.Now.Minute)
                    {
                        item.StartAuction();
                        foreach (var agent in Agents)
                        {
                            if (agent.WantToSub(item.Price, item.JumpOfPrice))
                            {
                                item.Agents.Add(agent);
                                item.Subscribe(agent);
                            }
                        }
                    }
                    item.RunAuction();
                }

            }

        }
    }
}
