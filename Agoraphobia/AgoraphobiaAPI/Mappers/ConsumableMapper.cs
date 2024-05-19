using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaLibrary;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace AgoraphobiaAPI.Mappers;

public static class ConsumableMapper
{
    public static Consumable ToConsumableFromCreateDto(this CreateConsumableRequestDto consumable)
    {
        return new Consumable(consumable.Name, consumable.Description, consumable.RarityIdx, consumable.Price, consumable.Energy,
            consumable.Hp, consumable.Defense, consumable.Attack, consumable.Duration, consumable.Sanity);
    }

    public static Consumable ToConsumableFromConsumableDto(this ConsumableDto consumableDto)
    {
        return new Consumable(consumableDto.Name, consumableDto.Description, consumableDto.RarityIdx, consumableDto.Price, consumableDto.Energy,
            consumableDto.Hp, consumableDto.Defense, consumableDto.Attack, consumableDto.Duration, consumableDto.Sanity);
    }

    public static ConsumableDto ToConsumableDto(this Consumable consumable)
    {
        return new ConsumableDto
        {
            Id = consumable.Id,
            Defense = consumable.Defense,
            Attack = consumable.Attack,
            Description = consumable.Description,
            Duration = consumable.Duration,
            Energy = consumable.Energy,
            Hp = consumable.Hp,
            RarityIdx = consumable.RarityIdx,
            Name = consumable.Name,
            Price = consumable.Price,
            Sanity = consumable.Sanity
        };
    }
}