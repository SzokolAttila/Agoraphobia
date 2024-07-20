using AgoraphobiaLibrary.JoinTables.Weapons;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Newtonsoft.Json;
using AgoraphobiaLibrary.JoinTables.Rooms;

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

        [JsonConstructor]
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

        public Weapon BuyWeapon(int index)
        {
            WeaponSale weapon = WeaponSales.ElementAt(index);
            if (weapon.Quantity == 1)
            {
                WeaponSales.RemoveAt(index);
            }
            else
            {
                weapon.Quantity--;
            }
            return weapon.Weapon;
        }

        public Armor BuyArmor(int index)
        {
            ArmorSale armor = ArmorSales.ElementAt(index);
            if (armor.Quantity == 1)
            {
                ArmorSales.RemoveAt(index);
            }
            else
            {
                armor.Quantity--;
            }
            return armor.Armor;
        }

        public Consumable BuyConsumable(int index)
        {
            ConsumableSale consumable = ConsumableSales.ElementAt(index);
            if (consumable.Quantity == 1)
            {
                ConsumableSales.RemoveAt(index);
            }
            else
            {
                consumable.Quantity--;
            }
            return consumable.Consumable;
        }

        [JsonIgnore]
        public List<WeaponSale> WeaponSales { get; set; } 
        [JsonIgnore]
        public List<ArmorSale> ArmorSales { get; set; } 
        [JsonIgnore]
        public List<ConsumableSale> ConsumableSales { get; set; }
        [JsonIgnore]
        public List<RoomMerchantArmorSaleStatus> RoomMerchantArmorSaleStatus { get; set; } = new();
        [JsonIgnore]
        public List<RoomMerchantWeaponSaleStatus> RoomMerchantWeaponSaleStatus { get; set; } = new();
        [JsonIgnore]
        public List<RoomMerchantConsumableSaleStatus> RoomMerchantConsumableSaleStatus { get; set; } = new();
    }
}
