using AgoraphobiaLibrary.JoinTables.Weapons;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Newtonsoft.Json;

namespace AgoraphobiaLibrary
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Merchant(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            WeaponSales = new();
            ArmorSales = new();
            ConsumableSales = new();
        }
        public Merchant(int id, string name, string description, List<WeaponSale> weaponSales, List<ArmorSale> armorSales, List<ConsumableSale> consumableSales)
        {
            Id = id;
            Name = name;
            Description = description;
            WeaponSales = weaponSales;
            ArmorSales = armorSales;
            ConsumableSales = consumableSales;
        }

        public Merchant(string name, string description)
        {
            Name = name;
            Description = description;
            ArmorSales = new();
            WeaponSales = new();
            ConsumableSales = new();
        }

        [JsonIgnore]
        public List<WeaponSale> WeaponSales { get; set; } 
        [JsonIgnore]
        public List<ArmorSale> ArmorSales { get; set; } 
        [JsonIgnore]
        public List<ConsumableSale> ConsumableSales { get; set; } 
    }
}
