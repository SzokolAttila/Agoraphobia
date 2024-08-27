using AgoraphobiaLibrary.JoinTables.Weapons;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Newtonsoft.Json;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.Exceptions.Player;

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

        public bool BuyWeapon(Weapon weapon, Player player)
        {
            if (player.DreamCoins < weapon.Price)
            {
                throw new NotEnoughDreamCoinsException();
            }

            WeaponInventory bought = new()
            {
                Weapon = weapon,
                WeaponId = weapon.Id,
                Quantity = 1,
                PlayerId = player.Id
            };
            player += bought;
            player.DreamCoins -= weapon.Price;

            WeaponSale weaponToSell = WeaponSales.Where(x => x.WeaponId == weapon.Id).First();
            if (weaponToSell.Quantity == 1)
            {
                WeaponSales.Remove(weaponToSell);
                return false;
            }

            weaponToSell.Quantity--;
            return true;
        }

        public bool BuyArmor(Armor armor, Player player)
        {
            if (player.DreamCoins < armor.Price)
            {
                throw new NotEnoughDreamCoinsException();
            }

            ArmorInventory bought = new()
            {
                Armor = armor,
                ArmorId = armor.Id,
                Quantity = 1,
                PlayerId = player.Id
            };
            player += bought;
            player.DreamCoins -= armor.Price;

            ArmorSale armorToSell = ArmorSales.Where(x => x.ArmorId == armor.Id).First();
            if (armorToSell.Quantity == 1)
            {
                ArmorSales.Remove(armorToSell);
                return false;
            }

            armorToSell.Quantity--;
            return true;
        }

        public bool BuyConsumable(Consumable consumable, Player player)
        {
            if (player.DreamCoins < consumable.Price)
            {
                throw new NotEnoughDreamCoinsException();
            }

            ConsumableInventory bought = new()
            {
                Consumable = consumable,
                ConsumableId = consumable.Id,
                Quantity = 1,
                PlayerId = player.Id
            };
            player += bought;
            player.DreamCoins -= consumable.Price;

            ConsumableSale consumableToSell = ConsumableSales.Where(x => x.ConsumableId == consumable.Id).First();
            if (consumableToSell.Quantity == 1)
            {
                ConsumableSales.Remove(consumableToSell);
                return false;
            }

            consumableToSell.Quantity--;
            return true;
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
