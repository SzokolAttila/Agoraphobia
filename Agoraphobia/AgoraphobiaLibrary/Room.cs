using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public class Room
    {
        private Random r = new Random();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<WeaponInventory> Weapons { get; set; }
        public List<ArmorInventory> Armors { get; set; }
        public List<ConsumableInventory> Consumables { get; set; }
        public RoomOrientation Orientation { get; set; }
        public int OrientationId { get => (int)Orientation; set => Orientation = (RoomOrientation)value; }
        //public Merchant Merchant { get; set; }
        public Enemy Enemy { get; set; }
        
        public enum RoomOrientation
        {
            Good,
            Neutral,
            Evil
        }

        public Room(int id, string name, string description, List<WeaponInventory> weapons, List<ArmorInventory> armors, List<ConsumableInventory> consumables, int orientationId, Enemy enemy)
        {
            Id = id;
            Name = name;
            Description = description;
            Weapons = weapons;
            Armors = armors;
            Consumables = consumables;
            OrientationId = orientationId;
            Enemy = enemy;
        }

        public Weapon PickupWeapon(int index)
        {
            WeaponInventory weapon = Weapons.ElementAt(index);
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
            ArmorInventory armor = Armors.ElementAt(index);
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
            ConsumableInventory consumable = Consumables.ElementAt(index);
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
    }
}
