namespace AgoraphobiaAPI.Dtos.Player;

public class PlayerDto
{
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
    public List<AgoraphobiaLibrary.Weapon> Weapons { get; set; } = new();
    public List<AgoraphobiaLibrary.Consumable> Consumables { get; set; } = new();
    public List<AgoraphobiaLibrary.Armor> Armors { get; set; } = new();
}