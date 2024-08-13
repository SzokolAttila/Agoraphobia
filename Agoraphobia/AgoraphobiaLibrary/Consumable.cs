using AgoraphobiaLibrary.Exceptions.Consumable;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public sealed record Consumable : Item
    {
        public int Energy { get; set; }

        public double Hp { get; set; }

        public double Defense { get; set; }

        public double Attack { get; set; }

        [JsonIgnore]
        private int duration;
        public int Duration
        {
            get
            {
                return duration;
            }
            set
            {
                if (value<0)
                {
                    throw new NegativeDurationException();
                }
                else
                {
                    duration = value;
                }
            }
        }
        
        public double Sanity { get; set; }

        [JsonConstructor]
        public Consumable(int id, string name, string description, int rarityIdx, int price,
            int energy, double hp, double defense, double attack, int duration, double sanity) : base(id, name, description, rarityIdx, price)
        {
            Energy = energy;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Duration = duration;
            Sanity = sanity;
        }

        public Consumable(string name, string description, int rarityIdx, int price,
            int energy, double hp, double defense, double attack, int duration, double sanity) : base(name, description, rarityIdx, price)
        {
            Energy = energy;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Duration = duration;
            Sanity = sanity;
        }
        [JsonIgnore]
        public List<ConsumableInventory> ConsumableInventories { get; set; } = new();

        [JsonIgnore]
        public List<ConsumableDroprate> ConsumableDroprates { get; set; } = new();
        [JsonIgnore]
        public List<ConsumableLoot> ConsumableLoots { get; set; } = new();
        [JsonIgnore]
        public List<ConsumableSale> ConsumableSales { get; set; } = new();
        [JsonIgnore]
        public List<Effect> Effects { get; set; } = new();
        [JsonIgnore]
        public List<RoomConsumableLootStatus> RoomConsumableLootStatus { get; set; } = new();
        [JsonIgnore]
        public List<RoomMerchantConsumableSaleStatus> RoomMerchantConsumableSaleStatus { get; set; } = new();
    }
}
