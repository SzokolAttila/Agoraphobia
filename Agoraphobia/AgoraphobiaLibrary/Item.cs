using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public abstract class Item : Element
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

        public ItemRarity Rarity { get; init; }
        public int Price { get; init; }
        public Item(int id, string name, string description, ItemRarity rarity, int price) : base(id, name, description)
        {
            Rarity = rarity;
            Price = price;
        }
    }
}
