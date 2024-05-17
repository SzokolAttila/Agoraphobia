using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorInventory;

public class ArmorInventoryDto
{
    public ArmorDto Armor { get; set; }
    public int Quantity { get; set; }
}