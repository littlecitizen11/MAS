using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public interface IBuilding
    {
        public string Name { get; set; }
        public int NumOfRooms { get; set; }
        public int RoomSize { get; set; }
        public bool HighWay { get; set; }
        public bool DisabledAccess { get; set; }
        public int ToiletNum { get; set; }
    }
}
