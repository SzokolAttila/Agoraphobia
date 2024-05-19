using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableInventory;

public class UpdateConsumableInventoryRequestDto
{
    public int PlayerId { get; set; }
    public ConsumableDto Consumable { get; set; } = new();
    public int Quantity { get; set; }
}