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
}