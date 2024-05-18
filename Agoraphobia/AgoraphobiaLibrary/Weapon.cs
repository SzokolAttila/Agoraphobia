using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public sealed record Weapon : Item
    {
        [JsonInclude]
        public double MinMultiplier { get; set; }

        [JsonInclude]
        public double MaxMultiplier { get; set; }

        [JsonInclude]
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

        public List<WeaponInventory> WeaponInventories { get; set; } = new();
    }
}
