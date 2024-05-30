using AgoraphobiaLibrary.Exceptions.Consumable;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public sealed record Consumable : Item
    {
        [JsonInclude]
        public int Energy { get; set; }

        [JsonInclude]
        public int Hp { get; set; }

        [JsonInclude]
        public int Defense { get; set; }

        [JsonInclude]
        public int Attack { get; set; }

        [JsonIgnore]
        private int duration;
        [JsonInclude]
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
        
        [JsonInclude]
        public int Sanity { get; set; }

        [JsonConstructor]
        public Consumable(int id, string name, string description, int rarityIdx, int price,
            int energy, int hp, int defense, int attack, int duration, int sanity) : base(id, name, description, rarityIdx, price)
        {
            Energy = energy;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Duration = duration;
            Sanity = sanity;
        }

        public Consumable(string name, string description, int rarityIdx, int price,
            int energy, int hp, int defense, int attack, int duration, int sanity) : base(name, description, rarityIdx, price)
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
