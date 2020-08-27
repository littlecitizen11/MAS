using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class Office : IBuilding
    {
        public string Name { get;  set; }
        public int NumOfRooms { get; set; }
        public int RoomSize { get; set; }
        public bool HighWay { get; set; }
        public bool DisabledAccess { get; set; }
        public int ToiletNum { get; set; }
        public Office(string name, int numofrooms, int roomsize, bool highway, bool disabledAccess, int toiletNum)
        {
            Name = name;
            NumOfRooms = numofrooms;
            RoomSize = roomsize;
            HighWay = highway;
            DisabledAccess = disabledAccess;
            ToiletNum = toiletNum;
        }
    }
}
