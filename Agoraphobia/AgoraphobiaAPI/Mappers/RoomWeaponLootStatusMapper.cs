using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaAPI.Dtos.RoomWeaponLootStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Mappers
{
    public static class RoomWeaponLootStatusMapper
    {
        public static RoomWeaponLootStatusDto ToRoomWeaponLootStatusDto(this RoomWeaponLootStatus status)
        {
            return new RoomWeaponLootStatusDto()
            {
                Weapon = status.Weapon!.ToWeaponDto(),
                PlayerId = status.PlayerId,
                Quantity = status.Quantity,
                RoomId = status.RoomId,
                Id = status.Id,
                WeaponId = status.WeaponId
            };
        }
    }
}
