using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Mappers
{
    public static class RoomArmorLootStatusMapper
    {
        public static RoomArmorLootStatusDto ToRoomArmorLootStatusDto(this RoomArmorLootStatus status)
        {
            return new RoomArmorLootStatusDto
            {
                Armor = status.Armor!.ToArmorDto(),
                PlayerId = status.PlayerId,
                Quantity = status.Quantity,
                ArmorId = status.ArmorId,
                RoomId = status.RoomId,
                Id = status.Id
            };
        }
    }
}
