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

        [JsonInclude]
        public int Duration { get; set; }

        [JsonConstructor]
        public Consumable(int id, string name, string description, int rarityIdx, int price,
            int energy, int hp, int defense, int attack, int duration) : base(id, name, description, rarityIdx, price)
        {
            Energy = energy;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Duration = duration;
        }

        public Consumable(string name, string description, int rarityIdx, int price,
            int energy, int hp, int defense, int attack, int duration) : base(name, description, rarityIdx, price)
        {
            Energy = energy;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Duration = duration;
        }
        [JsonIgnore]
        public List<ConsumableInventory> ConsumableInventories { get; set; } = new();
    }
}
