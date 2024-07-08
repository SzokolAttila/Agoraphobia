using Newtonsoft.Json;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Rooms;

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

        public int Defense { get; set; }
        
        public int Hp { get; set; }

        [JsonIgnore]
        public ArmorPiece ArmorType { get; set; }

        [JsonIgnore]
        public List<ArmorInventory> ArmorInventories { get; set; } = new();
        [JsonIgnore]
        public List<ArmorDroprate> ArmorDroprates { get; set; } = new();
        [JsonIgnore]
        public List<ArmorLoot> ArmorLoots { get; set; } = new();        
        [JsonIgnore]
        public List<ArmorSale> ArmorSales { get; set; } = new();
        [JsonIgnore]
        public List<RoomArmorLootStatus> RoomArmorLootStatus { get; set; } = new();
        [JsonIgnore]
        public List<RoomMerchantArmorSaleStatus> RoomMerchantArmorSaleStatus { get; set; } = new();

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
