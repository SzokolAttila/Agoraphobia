using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorLoot;

public class ArmorLootDto
{
    public ArmorDto Armor { get; set; } = new();
    public int Quantity { get; set; }
}