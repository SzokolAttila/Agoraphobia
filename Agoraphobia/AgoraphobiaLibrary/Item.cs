using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public abstract record Item : Element
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

        [JsonConstructor]
        public Item(string name, string description, int rarityIdx, int price) : base(name, description)
        {
            RarityIdx = rarityIdx;
            Price = price;
        }

        public Item(int id, string name, string description, int rarityIdx, int price) : base(id, name, description)
        {
            RarityIdx = rarityIdx;
            Price = price;
        }
    }
}
