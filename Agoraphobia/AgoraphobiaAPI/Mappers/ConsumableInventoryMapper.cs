using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Mappers;

public static class ConsumableInventoryMapper
{
    public static ConsumableInventoryDto ToConsumableInventoryDto(this ConsumableInventory consumableInventory)
    {
        return new ConsumableInventoryDto
        {
            Consumable = consumableInventory.Consumable!.ToConsumableDto(),
            Quantity = consumableInventory.Quantity,
            PlayerId = consumableInventory.PlayerId,
            ConsumableId = consumableInventory.ConsumableId,
            Id = consumableInventory.Id
        };
    }

    public static UpdateConsumableInventoryRequestDto ToUpdateConsumableInventoryRequestDto(
        this ConsumableInventory consumableInventory)
    {
        return new UpdateConsumableInventoryRequestDto
        {
            Quantity = consumableInventory.Quantity,
            Consumable = consumableInventory.Consumable!.ToConsumableDto(),
            PlayerId = consumableInventory.PlayerId
        };
    }
}