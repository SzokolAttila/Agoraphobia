using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaAPI.Dtos.Effect;

namespace AgoraphobiaAPI.Dtos.Effect;

public class EffectDto
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int ConsumableId { get; set; }
    public ConsumableDto Consumable { get; set; } = new();
    public int CurrentDuration { get; set; }
}