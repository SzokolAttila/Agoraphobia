using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableInventory;

public class ConsumableInventoryDto
{
    public int Id { get; set; }
    public int ConsumableId { get; set; }
    public int PlayerId { get; set; }
    public ConsumableDto Consumable { get; set; } = new();
    public int Quantity { get; set; }
}