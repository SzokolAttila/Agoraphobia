namespace AgoraphobiaLibrary;

public class Player
{
    public Player(int id, int accountId, double sanity, double health, int energy, double attack, double defense, int dreamCoins)
    {
        Id = id;
        AccountId = accountId;
        Sanity = sanity;
        Health = health;
        Energy = energy;
        Attack = attack;
        Defense = defense;
        DreamCoins = dreamCoins;
    }
    public int Id { get; private set; }
    public int AccountId { get; private set; }
    public double Sanity { get; private set; }
    public double Health { get; private set; }
    public int Energy { get; private set; }
    public double Attack { get; private set; }
    public double Defense { get; private set; }
    public int DreamCoins { get; private set; }
}