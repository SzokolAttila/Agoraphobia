using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using AgoraphobiaLibrary.Exceptions.Player;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Weapons;
using AgoraphobiaLibrary.Exceptions.Armor;
using System.Numerics;

namespace AgoraphobiaLibrary;

public class Player : INotifyPropertyChanged
{
    public Player(int accountId, int roomId, int slotId)
    {
        AccountId = accountId;
        RoomId = roomId;
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
        SlotId = slotId;
    }
    [JsonConstructor]
    public Player(int id, int accountId, double sanity, double maxHealth, double health, int maxEnergy, int energy, double attack, double defense, int dreamCoins, List<WeaponInventory> weapons, List<ConsumableInventory> consumables, List<ArmorInventory> armors, List<Effect> effects, int currentRoomId, int slotId) 
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
        Effects = effects;
        RoomId = currentRoomId;
        SlotId = slotId;
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
            _sanity = value;
            if ((value >= MAX_SANITY || value <= 0) && SanityOver != null)
                SanityOver(this, new EventArgs());
            OnPropertyChanged("Sanity");
        }
    }

    private double _maxHealth;
    public double MaxHealth 
    { 
        get { return _maxHealth; }
        set { _maxHealth = value; OnPropertyChanged("MaxHealth"); }
    }
    private double _health;
    public double Health
    {
        get => _health;
        set
        {
            if (value > MaxHealth)
                _health = MaxHealth;
            else if (value <= 0)
                DeathOccured(this, new EventArgs());
            else _health = value;
            OnPropertyChanged("Health");
        }
    }
    
    private int _maxEnergy;
    public int MaxEnergy 
    { 
        get { return _maxEnergy; }
        set { _maxEnergy = value; OnPropertyChanged("MaxEnergy"); }
    }
    
    private int _energy;
    public int Energy
    {
        get => _energy;
        set
        {
            if (value < 0)
                throw new NotEnoughEnergyException();
            _energy = value > MaxEnergy ? MaxEnergy : value;
            OnPropertyChanged("Energy");
        }
    }
    private double _attack;
    public double Attack 
    {
        get { return _attack; }
        set { _attack = value; OnPropertyChanged("Attack"); }
    }

    private double _defense;
    public double Defense
    {
        get { return _defense; }
        set { _defense = value; OnPropertyChanged("Defense"); }
    }
    private int _dreamCoins;
    public int DreamCoins
    {
        get => _dreamCoins;
        set
        {
            if (value < 0)
                throw new NotEnoughDreamCoinsException();
            _dreamCoins = value;
            OnPropertyChanged("DreamCoins");
        }
    }
    [ForeignKey("RoomId")]
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public int SlotId { get; set; }
    public List<WeaponInventory> WeaponInventories { get; set; }
    public List<ConsumableInventory> ConsumableInventories { get; set; }
    public List<Effect> Effects { get; set; } = new();
    public List<ArmorInventory> ArmorInventories { get; set; }

    [JsonIgnore]
    public EventHandler DeathOccured;
    [JsonIgnore]
    public EventHandler SanityOver;

    private const double BASE_SANITY = 30;
    private const double BASE_HEALTH = 42;
    private const int BASE_ENERGY = 3;
    private const double BASE_ATTACK = 4.5;
    private const double BASE_DEFENSE = 5.2;
    private const int BASE_DREAMCOINS = 100;
    private const int INVENTORY_CAPACITY = 50;
    private const int MAX_SANITY = 100;

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

    public bool AttackEnemy(Enemy target, Weapon weapon) // returns true if player dies
    {
        double dmg = (Random.Shared.NextDouble()*
                     (weapon.MaxMultiplier-weapon.MinMultiplier)+
                     weapon.MinMultiplier) * Attack;
        Energy -= weapon.Energy;
        if (target.TakeHit(dmg))
        {
            target.Death(this, Room);
        }
        else
        {
            Health = (target.Attack - Defense) > 0 ? Math.Round(Health - (target.Attack - Defense), 1) : Health;
            if (Health<=0)
            {
                return true;
            }
        }
        CheckEffects(false);
        return false;
    }

    private void CheckEffects(bool onlyRemove)
    {
        List<Effect> effects = new List<Effect>();
        foreach (var effect in Effects)
        {
            effects.Add(effect);
        }

        foreach (var effect in effects)
        {
            if (onlyRemove)
            {
                RemoveEffect(effect);
            }
            else
            {
                effect.CurrentDuration--;
                if (effect.CurrentDuration <= 0)
                {
                    RemoveEffect(effect);
                }
            }
        }
    }

    private void RemoveEffect(Effect effect)
    {
        Defense = Math.Round(Defense - effect.Consumable.Defense, 1);
        Attack = Math.Round(Attack - effect.Consumable.Attack);
        Effects.Remove(effect);
    }

    public void Death()
    {
        CheckEffects(true);
        Sanity -= Room.Enemy.Sanity;
        RoomId = 1;
        Energy = MaxEnergy;
        Health = MaxHealth;
        if (DreamCoins<=100)
        {
            DreamCoins = 0;
        }
        else
        {
            DreamCoins -= 100;
        }
    }

    public static Player operator +(Player player, ArmorInventory armor)
    {
        if ((player.InventoryCount+armor.Quantity)>INVENTORY_CAPACITY)
        {
            throw new InventoryAlreadyFullException();
        }

        if (player.ArmorInventories.Select(x=>x.Armor.ArmorType).Contains(armor.Armor.ArmorType))
        {
            throw new SameArmorTypeException();
        }

        if (player.ArmorInventories.Select(x=>x.ArmorId).Contains(armor.ArmorId))
        {
            player.ArmorInventories.First(x => x.ArmorId == armor.ArmorId).Quantity += armor.Quantity;
        }
        else
        {
            player.ArmorInventories.Add(armor);
        }

        player.AddArmorStats(armor.Armor);

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

    public bool DropArmor(Armor armor)
    {
        SubtractArmorStats(armor);
        Room.DropArmor(armor);
        
        ArmorInventory armorOfPlayer = ArmorInventories.First(x=>x.ArmorId == armor.Id);
        if (armorOfPlayer.Quantity>1)
        {
            ArmorInventories.First(x => x.ArmorId == armor.Id).Quantity--;
            return true; // still have some on true value
        }
        ArmorInventories.Remove(armorOfPlayer);
        return false;
    }

    public bool DropConsumable(Consumable consumable)
    {
        Room.DropConsumable(consumable);
        
        ConsumableInventory consumableOfPlayer = ConsumableInventories.First(x => x.ConsumableId == consumable.Id);
        if (consumableOfPlayer.Quantity > 1)
        {
            ConsumableInventories.First(x => x.ConsumableId == consumable.Id).Quantity--;
            return true; // still have some on true value
        }
        ConsumableInventories.Remove(consumableOfPlayer);
        return false;
    }

    public bool DropWeapon(Weapon weapon)
    {
        Room.DropWeapon(weapon);
        
        WeaponInventory weaponOfPlayer = WeaponInventories.First(x => x.WeaponId == weapon.Id);
        if (weaponOfPlayer.Quantity > 1)
        {
            WeaponInventories.First(x => x.WeaponId == weapon.Id).Quantity--;
            return true; // still have some on true value
        }
        WeaponInventories.Remove(weaponOfPlayer);
        return false;
    }

    public void AddArmorStats(Armor armor)
    {
        Defense += armor.Defense;
        MaxHealth += armor.Hp;
    }

    public void SubtractArmorStats(Armor armor)
    {
        Defense = Math.Round(Defense - armor.Defense, 1);
        MaxHealth = Math.Round(MaxHealth - armor.Hp, 1);
        //Health = Health;
    }

    public bool UseConsumable(Consumable consumable)
    {
        Effect effect = new Effect()
        {
            Consumable = consumable,
            ConsumableId = consumable.Id,
            Player = this,
            PlayerId = Id,
            CurrentDuration = consumable.Duration
        };
        Effects.Add(effect);

        if (Health < -consumable.Hp)
        {
            _sanity += Room.Enemy.Sanity; // modify private sanity field so player can't win from this
        }

        bool result = RemoveConsumable(consumable);

        Energy += consumable.Energy;
        Defense += consumable.Defense;
        Attack += consumable.Attack;
        Sanity += consumable.Sanity;
        Health += consumable.Hp;

        return result;
    }

    private bool RemoveConsumable(Consumable consumable)
    {
        ConsumableInventory consumableOfPlayer = ConsumableInventories.First(x => x.ConsumableId == consumable.Id);
        if (consumableOfPlayer.Quantity > 1)
        {
            ConsumableInventories.First(x => x.ConsumableId == consumable.Id).Quantity--;
            return true; // still have some on true value
        }
        ConsumableInventories.Remove(consumableOfPlayer);
        return false;
    }


    //For MVVM binding
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore] public List<RoomEnemyStatus> RoomEnemyStatusList { get; set; } = new();
    [JsonIgnore]
    public List<RoomArmorLootStatus> RoomArmorLootStatus { get; set; } = new();
    [JsonIgnore]
    public List<RoomWeaponLootStatus> RoomWeaponLootStatus { get; set; } = new();
    [JsonIgnore]
    public List<RoomConsumableLootStatus> RoomConsumableLootStatus { get; set; } = new();
    [JsonIgnore]
    public List<RoomMerchantArmorSaleStatus> RoomMerchantArmorSaleStatus { get; set; } = new();
    [JsonIgnore]
    public List<RoomMerchantWeaponSaleStatus> RoomMerchantWeaponSaleStatus { get; set; } = new();
    [JsonIgnore]
    public List<RoomMerchantConsumableSaleStatus> RoomMerchantConsumableSaleStatus { get; set; } = new();
}