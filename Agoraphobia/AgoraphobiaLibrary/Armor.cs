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
        public int Defense { get; init; }
        
        [JsonInclude]
        public int HP { get; init; }

        [JsonIgnore]
        public ArmorPiece ArmorType => (ArmorPiece)_armorTypeIdx;

        [JsonInclude]
        private int _armorTypeIdx { get; init; }
        
        public Armor(int id, string name, string description, int rarityIdx, int price,
            int defense, int hp, int armorTypeIdx) : base(id, name, description, rarityIdx, price)
        {
            Defense = defense;
            HP = hp;
            _armorTypeIdx = armorTypeIdx;
        }
    }
}
