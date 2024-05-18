using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgoraphobiaLibrary
{
    public class Enemy
    {
        private Random r = new Random();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<Weapon, double> Weapons { get; set; }
        public Dictionary<Armor, double> Armors { get; set; }
        public Dictionary<Consumable, double> Consumables { get; set; }
        public int Hp { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public int Sanity { get; set; }
        public int DreamCoins { get; set; }

        public Enemy(int id, string name, string description, int hp, int defense,
            int attack, int sanity, int coins, Dictionary<Weapon, double> weaponry,
            Dictionary<Armor, double> armory, Dictionary<Consumable, double> consumables)
        {
            Id = id;
            Name = name;
            Description = description;
            Defense = defense;
            Attack = attack;
            Sanity = sanity;
            DreamCoins = coins;
            Weapons = weaponry;
            Armors = armory;
            Consumables = consumables;
        }

        public Enemy(string name, string description, int hp, int defense,
            int attack, int sanity, int coins, Dictionary<Weapon, double> weaponry,
            Dictionary<Armor, double> armory, Dictionary<Consumable, double> consumables)
        {
            Name = name;
            Description = description;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Sanity = sanity;
            DreamCoins = coins;
            Weapons = weaponry;
            Armors = armory;
            Consumables = consumables;
        }

        public void Death(Player player)
        {
            player.Sanity += Sanity;
            player.DreamCoins += DreamCoins;
            //Drop Items in room
            //Remove itself from room
        }

        public List<Armor> DropArmors()
        {
            List<Armor> droppedArmors = new List<Armor>();
            foreach (KeyValuePair<Armor, double> armor in Armors)
            {
                if (r.NextDouble() <= armor.Value)
                {
                    droppedArmors.Add(armor.Key);
                }
            }
            return droppedArmors;
        }

        public List<Weapon> DropWeapons()
        {
            List<Weapon> droppedWeapons = new List<Weapon>();
            foreach (KeyValuePair<Weapon, double> weapon in Weapons)
            {
                if (r.NextDouble()<=weapon.Value)
                {
                    droppedWeapons.Add(weapon.Key);
                }
            }
            return droppedWeapons;
        }

        public List<Consumable> DropConsumables()
        {
            List<Consumable> droppedConsumables = new List<Consumable>();
            foreach (KeyValuePair<Consumable, double> consumable in Consumables)
            {
                if (r.NextDouble() <= consumable.Value)
                {
                    droppedConsumables.Add(consumable.Key);
                }
            }
            return droppedConsumables;
        }

        public bool TakeHit(int dmg)
        {
            if (dmg>Defense)
            {
                Hp -= (dmg-Defense);
                if (Hp<=0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
