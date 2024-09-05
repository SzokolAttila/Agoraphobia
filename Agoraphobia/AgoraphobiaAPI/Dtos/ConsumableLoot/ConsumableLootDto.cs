using AgoraphobiaAPI.Dtos.Consumable;

namespace AgoraphobiaAPI.Dtos.ConsumableLoot;

public class ConsumableLootDto
{
    public int Id { get; set; }
    public int ConsumableId { get; set; }
    public int RoomId { get; set; }
    public ConsumableDto Consumable { get; set; } = new();
    public int Quantity { get; set; }
}