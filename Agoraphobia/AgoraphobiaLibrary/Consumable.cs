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
        public int Energy { get; init; }

        [JsonInclude]
        public int HP { get; init; }

        [JsonInclude]
        public int Defense { get; init; }

        [JsonInclude]
        public int Attack { get; init; }

        [JsonInclude]
        public int Duration { get; init; }

        [JsonConstructor]
        public Consumable(int id, string name, string description, int rarityIdx, int price,
            int energy, int hp, int defense, int attack, int duration) : base(id, name, description, rarityIdx, price)
        {
            Energy = energy;
            HP = hp;
            Defense = defense;
            Attack = attack;
            Duration = duration;
        }
    }
}
