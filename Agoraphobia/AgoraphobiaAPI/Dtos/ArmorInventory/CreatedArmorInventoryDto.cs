using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorInventory;

public class CreatedArmorInventoryDto
{
    public int PlayerId { get; set; }
    public ArmorDto Armor { get; set; } = new();
    public int Quantity { get; set; }
}