namespace AgoraphobiaLibrary;

public class Player
{
    public Player(int accountId)
    {
        AccountId = accountId;
    }
    public Player(int id, int accountId, double sanity, double maxHealth, double health, int maxEnergy, int energy, double attack, double defense, int dreamCoins)
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
}