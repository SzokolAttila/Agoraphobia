using AgoraphobiaAPI.Dtos;
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
                Room = status.Room!.ToRoomDto()
            };
        }
    }
}
