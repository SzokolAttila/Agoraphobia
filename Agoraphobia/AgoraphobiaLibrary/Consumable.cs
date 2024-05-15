using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public sealed class Consumable : Item
    {
        public int Energy { get; init; }
        public int HP { get; init; }
        public int Defense { get; init; }
        public int Attack { get; init; }
        public int Duration { get; init; }

        public Consumable(int id, string name, string description, ItemRarity rarity, int price,
            int energy, int hp, int defense, int attack, int duration) : base(id, name, description, rarity, price)
        {
            Energy = energy;
            HP = hp;
            Defense = defense;
            Attack = attack;
            Duration = duration;
        }
    }
}
