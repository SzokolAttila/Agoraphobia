using AgoraphobiaAPI.Dtos.RoomConsumableLootStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Mappers
{
    public static class RoomConsumableLootStatusMapper
    {
        public static RoomConsumableLootStatusDto ToRoomConsumableLootStatusDto(this RoomConsumableLootStatus status)
        {
            return new RoomConsumableLootStatusDto()
            {
                Consumable = status.Consumable!.ToConsumableDto(),
                PlayerId = status.PlayerId,
                Quantity = status.Quantity,
                Room = status.Room!.ToRoomDto()
            };
        }
    }
}
