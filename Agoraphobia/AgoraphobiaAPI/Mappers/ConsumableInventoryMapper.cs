using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class ConsumableInventoryMapper
{
    public static ConsumableInventoryDto ToConsumableInventoryDto(this ConsumableInventory consumableInventory)
    {
        return new ConsumableInventoryDto
        {
            Consumable = consumableInventory.Consumable!.ToConsumableDto(),
            Quantity = consumableInventory.Quantity
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