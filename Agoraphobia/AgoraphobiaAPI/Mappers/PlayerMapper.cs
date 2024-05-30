using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class PlayerMapper
{
    public static Player ToAccountFromCreateDto(this CreatePlayerRequestDto playerDto)
    {
        return new Player(playerDto.AccountId, playerDto.RoomId);
    }

    public static PlayerDto ToPlayerDto(this Player player)
    {
        return new PlayerDto()
        {
            Id = player.Id,
            AccountId = player.AccountId,
            Attack = player.Attack,
            Defense = player.Defense,
            Sanity = player.Sanity,
            MaxHealth = player.MaxHealth,
            Health = player.Health,
            MaxEnergy = player.MaxEnergy,
            Energy = player.Energy,
            DreamCoins = player.DreamCoins,
            Effects = player.Effects.Select(x => x.ToEffectDto()).ToList(),
            Armors = player.ArmorInventories.Select(x => x.ToArmorInventoryDto()).ToList(),
            Weapons = player.WeaponInventories.Select(x => x.ToWeaponInventoryDto()).ToList(),
            Consumables = player.ConsumableInventories.Select(x => x.ToConsumableInventoryDto()).ToList(),
            CurrentRoom = player.Room!.ToRoomDto()
        };
    }
}