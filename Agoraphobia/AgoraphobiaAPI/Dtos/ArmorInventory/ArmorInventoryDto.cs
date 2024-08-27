using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorInventory;

public class ArmorInventoryDto
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int ArmorId { get; set; }
    public ArmorDto Armor { get; set; } = new();
    public int Quantity { get; set; }
}