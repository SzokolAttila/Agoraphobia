namespace AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus
{
    public class WeaponSaleStatusRequestDto
    {
        public int PlayerId { get; set; }
        public int WeaponId { get; set; }
        public int RoomId { get; set; }
        public int MerchantId { get; set; }
    }
}
