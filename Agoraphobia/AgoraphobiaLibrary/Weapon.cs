using AgoraphobiaLibrary.Exceptions.Weapon;
using AgoraphobiaLibrary.JoinTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AgoraphobiaLibrary.JoinTables.Weapons;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaLibrary
{
    public sealed record Weapon : Item
    {
        [JsonIgnore]
        private double minMultiplier = -1;
      
        public double MinMultiplier
        {
            get
            {
                return minMultiplier;
            }
            set
            {
                if (value < 0)
                {
                    throw new NegativeMaxOrMinException();
                }
                else if (value > MaxMultiplier && MaxMultiplier != -1)
                {
                    throw new MinGreaterThanMaxException();
                }
                else
                {
                    minMultiplier = value;
                }
            }
        }

        [JsonIgnore]
        private double maxMultiplier = -1;
        
        public double MaxMultiplier
        {
            get
            {
                return maxMultiplier;
            }
            set
            {
                if (value<0)
                {
                    throw new NegativeMaxOrMinException();
                }
                else if (value < MinMultiplier && MinMultiplier != -1)
                {
                    throw new MaxSmallerThanMinException();
                }
                else
                {
                    maxMultiplier = value;
                }
            }
        }

        public int Energy {  get; set; }

        [JsonConstructor]
        public Weapon(int id, string name, string description, int rarityIdx, int price,
            double minMultiplier, double maxMultiplier, int energy) : base(id,name,description,rarityIdx,price)
        {
            MinMultiplier = minMultiplier;
            MaxMultiplier = maxMultiplier;
            Energy = energy;
        }

        public Weapon(string name, string description, int rarityIdx, int price,
            double minMultiplier, double maxMultiplier, int energy) : base(name, description, rarityIdx, price)
        {
            MinMultiplier = minMultiplier;
            MaxMultiplier = maxMultiplier;
            Energy = energy;
        }
        [JsonIgnore]
        public List<WeaponInventory> WeaponInventories { get; set; } = new();
        [JsonIgnore]
        public List<WeaponDroprate> WeaponDroprates { get; set; } = new();
        [JsonIgnore]
        public List<WeaponLoot> WeaponLoots { get; set; } = new();
        [JsonIgnore]
        public List<WeaponSale> WeaponSales { get; set; } = new();
        [JsonIgnore]
        public List<RoomWeaponLootStatus> RoomWeaponLootStatus { get; set; } = new();
        [JsonIgnore]
        public List<RoomMerchantWeaponSaleStatus> RoomMerchantWeaponSaleStatus { get; set; } = new();
    }
}
