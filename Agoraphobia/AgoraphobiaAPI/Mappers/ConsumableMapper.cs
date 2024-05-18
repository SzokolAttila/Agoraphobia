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

    public static ConsumableDto ToConsumableDto(this Consumable consumable)
    {
        return new ConsumableDto
        {
            Defense = consumable.Defense,
            Attack = consumable.Attack,
            Description = consumable.Description,
            Duration = consumable.Duration,
            Energy = consumable.Energy,
            Hp = consumable.Hp,
            RarityIdx = consumable.RarityIdx,
            Name = consumable.Name,
            Price = consumable.Price,
            Id = consumable.Id
        };
    }
}