using System.Text.Json.Serialization;

namespace AgoraphobiaLibrary;

public class Player
{
    public Player(int accountId)
    {
        AccountId = accountId;
        Weapons = new List<Weapon>();
        Health = BASE_HEALTH;
        MaxHealth = BASE_HEALTH;
        Energy = BASE_ENERGY;
        MaxEnergy = BASE_ENERGY;
        Sanity = BASE_SANITY;
        Defense = BASE_DEFENSE;
        Attack = BASE_ATTACK;
        DreamCoins = BASE_DREAMCOINS;
    }
    [JsonConstructor]
    public Player(int id, int accountId, double sanity, double maxHealth, double health, int maxEnergy, int energy, double attack, double defense, int dreamCoins, List<Weapon> weapons)
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
        Weapons = weapons;
    }
    public int Id { get; set; }
    public int AccountId { get; set; }
    public double Sanity { get; set; }
    public double MaxHealth { get; set; }
    public double Health { get; set; }
    public int MaxEnergy { get; set; }
    public int Energy { get; set; }
    public double Attack { get; set; }
    public double Defense { get; set; }
    public int DreamCoins { get; set; }
    public List<Weapon> Weapons { get; set; }
    private const double BASE_SANITY = 30;
    private const double BASE_HEALTH = 42;
    private const int BASE_ENERGY = 3;
    private const double BASE_ATTACK = 4.5;
    private const double BASE_DEFENSE = 5.2;
    private const int BASE_DREAMCOINS = 100;
}