using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public abstract record Item
    {
        public enum ItemRarity
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary,
            Fabled
        }

        [JsonInclude]
        public int RarityIdx { get => (int)Rarity; set => Rarity = (ItemRarity)value; }

        [JsonIgnore]
        public ItemRarity Rarity { get; set; }

        [JsonInclude]
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        [JsonConstructor]
        protected Item(string name, string description, int rarityIdx, int price)
        {
            RarityIdx = rarityIdx;
            Price = price;
            Name = name;
            Description = description;
        }

        protected Item(int id, string name, string description, int rarityIdx, int price)
        {
            RarityIdx = rarityIdx;
            Price = price;
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
