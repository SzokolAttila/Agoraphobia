using AgoraphobiaAPI.Dtos.ConsumableLoot;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Mappers;

public static class ConsumableLootMapper
{
    public static ConsumableLootDto ToConsumableLootDto(this ConsumableLoot consumableLoot)
    {
        return new ConsumableLootDto
        {
            Quantity = consumableLoot.Quantity,
            Consumable = consumableLoot.Consumable!.ToConsumableDto(),
            RoomId = consumableLoot.RoomId,
            ConsumableId = consumableLoot.ConsumableId,
            Id = consumableLoot.Id
        };
    }
}