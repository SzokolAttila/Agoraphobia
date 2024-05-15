using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class ConsumableMapper
{
    public static Consumable ToConsumableFromCreateDto(this CreateConsumableRequestDto consumable)
    {
        return new Consumable(consumable.Name, consumable.Description, consumable.RarityIdx, consumable.Price, consumable.Energy,
            consumable.Hp, consumable.Defense, consumable.Attack, consumable.Duration);
    }
}