using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AgoraphobiaLibrary.Exceptions.Player;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaLibrary;

public class Player
{
    public Player(int accountId)
    {
        AccountId = accountId;
        WeaponInventories = new List<WeaponInventory>();
        ArmorInventories = new List<ArmorInventory>();
        ConsumableInventories = new List<ConsumableInventory>();
        MaxHealth = BASE_HEALTH;
        Health = BASE_HEALTH;
        MaxEnergy = BASE_ENERGY;
        Energy = BASE_ENERGY;
        Sanity = BASE_SANITY;
        Defense = BASE_DEFENSE;
        Attack = BASE_ATTACK;
        DreamCoins = BASE_DREAMCOINS;
    }
    [JsonConstructor]
    public Player(int id, int accountId, double sanity, double maxHealth, double health, int maxEnergy, int energy, double attack, double defense, int dreamCoins, List<WeaponInventory> weapons, List<ConsumableInventory> consumables, List<ArmorInventory> armors)
    {
        Id = id;
        AccountId = accountId;
        Sanity = sanity;
        MaxHealth = maxHealth;
        Health = health;
        MaxEnergy = maxEnergy;
        Energy = energy;
        Attack = attack;
        Defense = defense;
        DreamCoins = dreamCoins;
        WeaponInventories = weapons;
        ArmorInventories = armors;
        ConsumableInventories = consumables;
    }
    public int Id { get; set; }
    [ForeignKey("AccountId")]
    public int AccountId { get; set; }
    public Account? Account { get; set; }
    private double _sanity;
    public double Sanity
    {
        get => _sanity;
        set
        {
            if (value > MAX_SANITY)
                _sanity = MAX_SANITY;
            else if (value < 0)
                _sanity = 0;
            else _sanity = value;
        }
    }
    public double MaxHealth { get; set; }
    private double _health;
    public double Health
    {
        get => _health;
        set
        {
            if (value > MaxHealth)
                _health = MaxHealth;
            else if (value < 0)
                _health = 0;
            else _health = value;
        }
    }
    public int MaxEnergy { get; set; }
    private int _energy;
    public int Energy
    {
        get => _energy;
        set
        {
            if (value < 0)
                throw new NotEnoughEnergyException();
            _energy = value > MaxEnergy ? MaxEnergy : value;
        }
    }
    public double Attack { get; set; }
    public double Defense { get; set; }
    private int _dreamCoins;
    public int DreamCoins
    {
        get => _dreamCoins;
        set
        {
            if (value < 0)
                throw new NotEnoughDreamCoinsException();
            _dreamCoins = value;
        }
    }
    public List<WeaponInventory> WeaponInventories { get; set; }
    public List<ConsumableInventory> ConsumableInventories { get; set; }
    public List<Effect> Effects { get; set; } = new();
    public List<ArmorInventory> ArmorInventories { get; set; }

    private const double BASE_SANITY = 30;
    private const double BASE_HEALTH = 42;
    private const int BASE_ENERGY = 3;
    private const double BASE_ATTACK = 4.5;
    private const double BASE_DEFENSE = 5.2;
    private const int BASE_DREAMCOINS = 100;
    private const int INVENTORY_CAPACITY = 50;
    private const int MAX_SANITY = 120;

    public int InventoryCount
    {
        get
        {
            int sum = 0;
            foreach (var item in WeaponInventories)
            {
                sum += item.Quantity;
            }
            foreach (var item in ConsumableInventories)
            {
                sum += item.Quantity;
            }
            foreach (var item in ArmorInventories)
            {
                sum += item.Quantity;
            }
            return sum;
        }
    }

    public void AttackEnemy(Enemy target, Weapon weapon)
    {
        double dmg = (Random.Shared.NextDouble()*(weapon.MaxMultiplier-weapon.MinMultiplier)+weapon.MinMultiplier) * Attack;
        Energy -= weapon.Energy;
        if (target.TakeHit(dmg))
        {
            target.Death(this);
        }
        else
        {
            Health -= target.Attack;
        }

    }

    public static Player operator +(Player player, ArmorInventory armor)
    {
        if ((player.InventoryCount+armor.Quantity)>INVENTORY_CAPACITY)
        {
            throw new InventoryAlreadyFullException();
        }

        if (player.ArmorInventories.Select(x=>x.ArmorId).Contains(armor.ArmorId))
        {
            player.ArmorInventories.First(x => x.ArmorId == armor.ArmorId).Quantity += armor.Quantity;
        }
        else
        {
            player.ArmorInventories.Add(armor);
        }

        return player;
    }

    public static Player operator +(Player player, ConsumableInventory consumable)
    {
        if ((player.InventoryCount + consumable.Quantity) > INVENTORY_CAPACITY)
        {
            throw new InventoryAlreadyFullException();
        }

        if (player.ConsumableInventories.Select(x => x.ConsumableId).Contains(consumable.ConsumableId))
        {
            player.ConsumableInventories.First(x => x.ConsumableId == consumable.ConsumableId).Quantity += consumable.Quantity;
        }
        else
        {
            player.ConsumableInventories.Add(consumable);
        }

        return player;
    }


    public static Player operator +(Player player, WeaponInventory weapon)
    {
        if ((player.InventoryCount + weapon.Quantity) > INVENTORY_CAPACITY)
        {
            throw new InventoryAlreadyFullException();
        }

        if (player.WeaponInventories.Select(x => x.WeaponId).Contains(weapon.WeaponId))
        {
            player.WeaponInventories.First(x => x.WeaponId == weapon.WeaponId).Quantity += weapon.Quantity;
        }
        else
        {
            player.WeaponInventories.Add(weapon);
        }

        return player;
    }
}