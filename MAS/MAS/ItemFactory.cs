﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class ItemFactory
    {
        //public List<IItem> ItemLists { get; set; }
        public List<IItem> GetBuildingLists()
        {
            List<IItem> ItemLists = new List<IItem>();
            ItemLists.Add(new Office("Feldman", 2, 10, true, false, 2));
            ItemLists.Add(new Resident("Carmel's House", 2, 10, true, false, 2));
            return ItemLists;
            
        }
    }
}