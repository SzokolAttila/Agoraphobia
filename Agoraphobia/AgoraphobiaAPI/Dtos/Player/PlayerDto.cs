using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaAPI.Dtos.WeaponInventory;

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
    public List<WeaponInventoryDto> Weapons { get; set; } = new();
    public List<ConsumableInventoryDto> Consumables { get; set; } = new();
    public List<ArmorInventoryDto> Armors { get; set; } = new();
}