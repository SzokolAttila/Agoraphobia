using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public sealed record Armor : Item
    {
        public enum ArmorPiece
        {
            Helmet,
            Chestplate,
            Gauntlet,
            Boots
        }

        [JsonInclude]
        public int Defense { get; set; }
        
        [JsonInclude]
        public int Hp { get; set; }

        [JsonIgnore]
        public ArmorPiece ArmorType { get; set; }

        [JsonInclude]
        public int ArmorTypeIdx { get => (int)ArmorType; set => ArmorType = (ArmorPiece)value; }
        [JsonConstructor]
        public Armor(int id, string name, string description, int rarityIdx, int price,
            int defense, int hp, int armorTypeIdx) : base(id, name, description, rarityIdx, price)
        {
            Defense = defense;
            Hp = hp;
            ArmorTypeIdx = armorTypeIdx;
        }

        public Armor(string name, string description, int rarityIdx, int price,
            int defense, int hp, int armorTypeIdx) : base(name, description, rarityIdx, price)
        {
            Defense = defense;
            Hp = hp;
            ArmorTypeIdx = armorTypeIdx;
        }
    }
}
