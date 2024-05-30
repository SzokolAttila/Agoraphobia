using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaLibrary
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<WeaponLoot> Weapons { get; set; }
        public List<ArmorLoot> Armors { get; set; }
        public List<ConsumableLoot> Consumables { get; set; }
        [JsonIgnore]
        public RoomOrientation Orientation { get; set; }
        public int OrientationId { get => (int)Orientation; set => Orientation = (RoomOrientation)value; }
        [ForeignKey("EnemyId")]
        public int EnemyId { get; set; }
        [JsonIgnore]
        public Enemy? Enemy { get; set; }
        [ForeignKey("MerchantId")]
        public int MerchantId { get; set; }
        [JsonIgnore] public Merchant? Merchant { get; set; }

        [JsonIgnore]
        public List<RoomArmorLootStatus> RoomArmorLootStatus { get; set; } = new();
        [JsonIgnore]
        public List<RoomWeaponLootStatus> RoomWeaponLootStatus { get; set; } = new();
        [JsonIgnore]
        public List<RoomConsumableLootStatus> RoomConsumableLootStatus { get; set; } = new();
        [JsonIgnore]
        public List<RoomMerchantArmorSaleStatus> RoomMerchantArmorSaleStatus { get; set; } = new();
        public enum RoomOrientation
        {
            Good,
            Neutral,
            Evil
        }

        [JsonConstructor]
        public Room(int id, string name, string description, List<WeaponLoot> weapons,
            List<ArmorLoot> armors, List<ConsumableLoot> consumables, int orientationId, int enemyId, int merchantId)
        {
            Id = id;
            Name = name;
            Description = description;
            Weapons = weapons;
            Armors = armors;
            Consumables = consumables;
            OrientationId = orientationId;
            EnemyId = enemyId;
            MerchantId = merchantId;
        }
        public Room(string name, string description, List<WeaponLoot> weapons,
            List<ArmorLoot> armors, List<ConsumableLoot> consumables, int orientationId, int enemyId, int merchantId)
        {
            Name = name;
            Description = description;
            Weapons = weapons;
            Armors = armors;
            Consumables = consumables;
            OrientationId = orientationId;
            EnemyId = enemyId;
            MerchantId = merchantId;
        }

        public Room(string name, string description, int orientationId, int enemyId, int merchantId)
        {
            Name = name;
            Description = description;
            Weapons = new List<WeaponLoot>();
            Armors = new List<ArmorLoot>();
            Consumables = new List<ConsumableLoot>();
            OrientationId = orientationId;
            EnemyId = enemyId;
            MerchantId = merchantId;
        }

        public Weapon PickupWeapon(int index)
        {
            WeaponLoot weapon = Weapons.ElementAt(index);
            if (weapon.Quantity == 1)
            {
                Weapons.RemoveAt(index);
            }
            else
            {
                weapon.Quantity--;
            }
            return weapon.Weapon;
        }

        public Armor PickupArmor(int index)
        {
            ArmorLoot armor = Armors.ElementAt(index);
            if (armor.Quantity == 1)
            {
                Armors.RemoveAt(index);
            }
            else
            {
                armor.Quantity--;
            }
            return armor.Armor;
        }

        public Consumable PickupConsumable(int index)
        {
            ConsumableLoot consumable = Consumables.ElementAt(index);
            if (consumable.Quantity == 1)
            {
                Consumables.RemoveAt(index);
            }
            else
            {
                consumable.Quantity--;
            }
            return consumable.Consumable;
        }

        public void DropWeapon(Weapon weapon)
        {
            List<WeaponLoot> wls = Weapons.Where(x => x.Weapon.Id == weapon.Id).ToList();
            if (wls.Count() == 0)
            {
                WeaponLoot wl = new WeaponLoot()
                {
                    Weapon = weapon,
                    WeaponId = weapon.Id,
                    Quantity = 1,
                    Room = this,
                    RoomId = Id
                };
                Weapons.Add(wl);
            }
            else
            {
                wls.First().Quantity++;
            }
        }

        public void DropArmor(Armor armor)
        {
            List<ArmorLoot> als = Armors.Where(x => x.Armor.Id == armor.Id).ToList();
            if (als.Count() == 0)
            {
                ArmorLoot al = new ArmorLoot()
                {
                    Armor = armor,
                    ArmorId = armor.Id,
                    Quantity = 1,
                    Room = this,
                    RoomId = Id
                };
                Armors.Add(al);
            }
            else
            {
                als.First().Quantity++;
            }
        }

        public void DropConsumable(Consumable consumable)
        {
            List<ConsumableLoot> cls = Consumables.Where(x => x.Consumable.Id == consumable.Id).ToList();
            if (cls.Count() == 0)
            {
                ConsumableLoot cl = new ConsumableLoot()
                {
                    Consumable = consumable,
                    ConsumableId = consumable.Id,
                    Quantity = 1,
                    Room = this,
                    RoomId = Id
                };
                Consumables.Add(cl);
            }
            else
            {
                cls.First().Quantity++;
            }
        }
        [JsonIgnore] public List<RoomEnemyStatus> RoomEnemyStatusList { get; set; } = new();
    }
}
