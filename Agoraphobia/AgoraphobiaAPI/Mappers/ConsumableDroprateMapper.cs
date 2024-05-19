using AgoraphobiaAPI.Dtos.ConsumableDroprate;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class ConsumableDroprateMapper
{
    public static ConsumableDroprateDto ToConsumableDroprateDto(this ConsumableDroprate consumableDroprate)
    {
        return new ConsumableDroprateDto
        {
            Consumable = consumableDroprate.Consumable!.ToConsumableDto(),
            Droprate = consumableDroprate.Droprate
        };
    }

    public static ConsumableDroprate ToConsumableDroprate(this ConsumableDroprateDto consumableDroprateDto)
    {
        return new ConsumableDroprate
        {
            Consumable = consumableDroprateDto.Consumable.ToConsumableFromConsumableDto(),
            Droprate = consumableDroprateDto.Droprate
        };
    }

    public static UpdateConsumableDroprateRequestDto ToUpdateConsumableDroprateRequestDto(this ConsumableDroprate consumableDroprate)
    {
        return new UpdateConsumableDroprateRequestDto
        {
            EnemyId = consumableDroprate.EnemyId,
            Consumable = consumableDroprate.Consumable!.ToConsumableDto(),
            Droprate = consumableDroprate.Droprate
        };
    }
}