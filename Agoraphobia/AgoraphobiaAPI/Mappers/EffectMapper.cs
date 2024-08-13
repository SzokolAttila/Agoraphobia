using AgoraphobiaAPI.Dtos.Effect;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class EffectMapper
{
    public static EffectDto ToEffectDto(this Effect effect)
    {
        return new EffectDto
        {
            CurrentDuration = effect.CurrentDuration,
            Consumable = effect.Consumable!.ToConsumableDto(),
            PlayerId = effect.PlayerId,
            ConsumableId = effect.ConsumableId,
            Id = effect.Id
        };
    }

    public static UpdateEffectRequestDto ToUpdateEffectRequestDto(this Effect effect)
    {
        return new UpdateEffectRequestDto
        {
            Consumable = effect.Consumable!.ToConsumableDto(),
            PlayerId = effect.PlayerId,
            CurrentDuration = effect.CurrentDuration
        };
    }
}