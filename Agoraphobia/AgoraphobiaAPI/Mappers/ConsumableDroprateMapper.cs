using AgoraphobiaAPI.Dtos.ConsumableDroprate;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Mappers;

public static class ConsumableDroprateMapper
{
    public static ConsumableDroprateDto ToConsumableDroprateDto(this ConsumableDroprate consumableDroprate)
    {
        return new ConsumableDroprateDto
        {
            Consumable = consumableDroprate.Consumable!.ToConsumableDto(),
            Droprate = consumableDroprate.Droprate,
            ConsumableId = consumableDroprate.ConsumableId,
            EnemyId = consumableDroprate.EnemyId,
            Id = consumableDroprate.Id,
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