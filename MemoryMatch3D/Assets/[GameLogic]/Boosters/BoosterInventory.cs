using System;
using System.Collections.Generic;
using System.Linq;
using Ui.Elements;

namespace Boosters
{
    public class BoosterInventory
    {
        public BoosterInventory()
        {
            Inventory = new Dictionary<Type, short>();
        }

        public Dictionary<Type, short> Inventory;

        public bool Add(Type boosterType, short count = 1)
        {
            var hasType = Inventory.ContainsKey(boosterType);
            if (hasType) Inventory[boosterType]++;
            else Inventory.Add(boosterType,1);
            return hasType;
        }

        public bool Get(Type boosterType)
        {
            if (!Inventory.ContainsKey(boosterType)) return false;
            if (Inventory[boosterType] == 0) return false;
            Inventory[boosterType]--;
            return true;
        }
        
    }
}