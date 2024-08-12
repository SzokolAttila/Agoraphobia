using AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus;
using AgoraphobiaLibrary.JoinTables.Rooms;

namespace AgoraphobiaAPI.Mappers
{
    public static class RoomMerchantWeaponSaleStatusMapper
    {
        public static RoomMerchantWeaponSaleStatusDto ToRoomMerchantWeaponSaleStatusDto(
            this RoomMerchantWeaponSaleStatus status)
        {
            return new RoomMerchantWeaponSaleStatusDto
            {
                Weapon = status.Weapon!.ToWeaponDto(),
                Quantity = status.Quantity,
                PlayerId = status.PlayerId,
                Id = status.Id,
                RoomId = status.RoomId,
                WeaponId = status.WeaponId,
                MerchantId = status.MerchantId
            };
        }
    }
}
