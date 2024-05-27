using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaAPI.Dtos.Effect;

namespace AgoraphobiaAPI.Dtos.Effect;

public class UpdateEffectRequestDto
{
    public int PlayerId { get; set; }
    public ConsumableDto Consumable { get; set; } = new();
    public int CurrentDuration { get; set; }
}