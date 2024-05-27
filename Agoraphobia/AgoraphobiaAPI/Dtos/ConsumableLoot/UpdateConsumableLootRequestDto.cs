using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableLoot;

public class UpdateConsumableLootRequestDto
{
    public int RoomId { get; set; }
    public ConsumableDto Consumable { get; set; } = new();
    public int Quantity { get; set; }
}