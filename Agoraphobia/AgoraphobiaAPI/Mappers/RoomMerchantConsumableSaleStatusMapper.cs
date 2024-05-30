using AgoraphobiaAPI.Dtos.RoomMerchantConsumableSaleStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Mappers
{
    public static class RoomMerchantConsumableSaleStatusMapper
    {
        public static RoomMerchantConsumableSaleStatusDto ToRoomMerchantConsumableSaleStatusDto(
            this RoomMerchantConsumableSaleStatus status)
        {
            return new RoomMerchantConsumableSaleStatusDto
            {
                Consumable = status.Consumable!.ToConsumableDto(),
                Quantity = status.Quantity,
                PlayerId = status.PlayerId,
                Room = status.Room!.ToRoomDto()
            };
        }
    }
}
