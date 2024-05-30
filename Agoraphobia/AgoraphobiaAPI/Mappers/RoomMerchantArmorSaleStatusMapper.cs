using AgoraphobiaAPI.Dtos.RoomMerchantArmorSaleStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Mappers
{
    public static class RoomMerchantArmorSaleStatusMapper
    {
        public static RoomMerchantArmorSaleStatusDto ToRoomMerchantArmorSaleStatusDto(
            this RoomMerchantArmorSaleStatus status)
        {
            return new RoomMerchantArmorSaleStatusDto
            {
                Armor = status.Armor!.ToArmorDto(),
                Quantity = status.Quantity,
                PlayerId = status.PlayerId,
                Room = status.Room!.ToRoomDto()
            };
        }
    }
}
