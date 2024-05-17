using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorInventory;

public class UpdateArmorInventoryRequestDto
{
    public int PlayerId { get; set; }
    public ArmorDto Armor { get; set; } = new();
    public int Quantity { get; set; }
}