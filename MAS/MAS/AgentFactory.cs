using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class AgentFactory
    {
        public List<IAgent> AgentList { get; set; }
        public AgentFactory()
        {
            AgentList = new List<IAgent>();
            AgentList.Add(new Agent((AgentList.Count+1), 25,"Yuval the man"));
            AgentList.Add(new Agent((AgentList.Count + 1), 3, "Revivo"));
            AgentList.Add(new Agent((AgentList.Count + 1), 22, "Assa"));
            AgentList.Add(new Agent((AgentList.Count + 1), 25, "Tamar"));
            AgentList.Add(new Agent((AgentList.Count + 1), 3, "Talevy"));
            AgentList.Add(new Agent((AgentList.Count + 1), 22, "Balba"));
        }
    }
}
