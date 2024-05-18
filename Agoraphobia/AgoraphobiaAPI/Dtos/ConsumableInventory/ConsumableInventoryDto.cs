using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableInventory;

public class ConsumableInventoryDto
{
    public ConsumableDto Consumable { get; set; } = new();
    public int Quantity { get; set; }
}