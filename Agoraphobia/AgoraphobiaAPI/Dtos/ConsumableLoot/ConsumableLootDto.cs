using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableLoot;

public class ConsumableLootDto
{
    public ConsumableDto Consumable { get; set; } = new();
    public int Quantity { get; set; }
}