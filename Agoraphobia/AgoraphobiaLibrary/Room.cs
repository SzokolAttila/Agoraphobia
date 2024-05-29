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
        private Random r = new Random();

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

        [JsonIgnore] 
        public List<RoomEnemyStatus> RoomEnemyStatusList { get; set; } = new();
    }
}
