using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaAPI.Dtos.Effect;

namespace AgoraphobiaAPI.Dtos.Effect;

public class EffectDto
{
    public ConsumableDto Consumable { get; set; } = new();
    public int CurrentDuration { get; set; }
}