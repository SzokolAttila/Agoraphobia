using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class RoomMapper
{
    public static Room ToRoomFromCreateDto(this CreateRoomRequestDto roomDto)
    {
        return new Room(roomDto.Name, roomDto.Description, roomDto.OrientationId, roomDto.EnemyId, roomDto.MerchantId);
    }

    public static RoomDto ToRoomDto(this Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            Description = room.Description,
            OrientationId = room.OrientationId,
            Merchant = room.Merchant!.ToMerchantDto(),
            MerchantId = room.MerchantId, 
            Enemy = room.Enemy!.ToEnemyDto(),
            Weapons = room.Weapons.Select(x => x.ToWeaponLootDto()).ToList(),
            Armors = room.Armors.Select(x => x.ToArmorLootDto()).ToList(),
            Consumables = room.Consumables.Select(x => x.ToConsumableLootDto()).ToList(),
        };
    }
}