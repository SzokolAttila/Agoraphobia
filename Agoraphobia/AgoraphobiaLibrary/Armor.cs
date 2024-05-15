using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public sealed class Armor : Item
    {
        public enum ArmorPiece
        {
            Helmet,
            Chestplate,
            Gauntlet,
            Boots
        }
        public int Defense { get; init; }
        public int HP { get; init; }
        public ArmorPiece ArmorType { get; init; }
        
        public Armor(int id, string name, string description, ItemRarity rarity, int price,
            int defense, int hp, ArmorPiece armorType) : base(id, name, description, rarity, price)
        {
            Defense = defense;
            HP = hp;
            ArmorType = armorType;
        }
    }
}
