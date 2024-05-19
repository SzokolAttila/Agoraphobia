using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace AgoraphobiaLibrary
{
    public class Enemy
    {
        private Random r = new Random();

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<WeaponDroprate> WeaponDroprates { get; set; }
        [NotMapped]
        public Dictionary<Consumable, double> Consumables { get; set; }
        public List<ArmorDroprate> ArmorDroprates { get; set; } = new();
        public double Hp { get; set; }
        public double Defense { get; set; }
        public double Attack { get; set; }
        public double Sanity { get; set; }
        public int DreamCoins { get; set; }

        [JsonConstructor]
        public Enemy(int id, string name, string description, double hp, double defense,
            double attack, double sanity, int dreamCoins, List<WeaponDroprate> weaponDroprates,
            List<ArmorDroprate> armorDroprates, Dictionary<Consumable, double> consumables)
        {
            Id = id;
            Name = name;
            Description = description;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Sanity = sanity;
            DreamCoins = dreamCoins;
            WeaponDroprates = weaponDroprates;
            ArmorDroprates = armorDroprates;
            Consumables = consumables;
        }

        public Enemy(string name, string description, double hp, double defense,
            double attack, double sanity, int dreamCoins, List<WeaponDroprate> weaponDroprates,
            List<ArmorDroprate> armorDroprates, Dictionary<Consumable, double> consumables)
        {
            Name = name;
            Description = description;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Sanity = sanity;
            DreamCoins = dreamCoins;
            WeaponDroprates = weaponDroprates;
            ArmorDroprates = armorDroprates;
            Consumables = consumables;
        }

        public Enemy(string name, string description, double hp, double defense,
            double attack, double sanity, int dreamCoins)
        {
            Name = name;
            Description = description;
            Hp = hp;
            Defense = defense;
            Attack = attack;
            Sanity = sanity;
            DreamCoins = dreamCoins;
            WeaponDroprates = new List<WeaponDroprate>();
            ArmorDroprates = new List<ArmorDroprate>();
            Consumables = new Dictionary<Consumable, double>();
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
            foreach (ArmorDroprate armor in ArmorDroprates)
            {
                if (r.NextDouble() <= armor.Droprate)
                {
                    droppedArmors.Add(armor.Armor);
                }
            }
            return droppedArmors;
        }

        public List<Weapon> DropWeaponDroprates()
        {
            List<Weapon> droppedWeaponDroprates = new List<Weapon>();
            foreach (WeaponDroprate weapon in WeaponDroprates)
            {
                if (r.NextDouble()<=weapon.Droprate)
                {
                    droppedWeaponDroprates.Add(weapon.Weapon);
                }
            }
            return droppedWeaponDroprates;
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
