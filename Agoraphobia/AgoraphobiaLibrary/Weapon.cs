using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public sealed class Weapon : Item
    {
        public double MinMultiplier { get; init; }
        public double MaxMultiplier { get; init; }
        public int Energy {  get; init; }

        public Weapon(int id, string name, string description, ItemRarity rarity, int price,
            double minMultiplier, double maxMultiplier, int energy) : base(id,name,description,rarity,price)
        {
            MinMultiplier = minMultiplier;
            MaxMultiplier = maxMultiplier;
            Energy = energy;
        }
    }
}
