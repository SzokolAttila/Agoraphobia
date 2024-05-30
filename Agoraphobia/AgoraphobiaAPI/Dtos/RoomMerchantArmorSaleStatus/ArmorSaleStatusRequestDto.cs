namespace AgoraphobiaAPI.Dtos.RoomMerchantArmorSaleStatus
{
    public class ArmorSaleStatusRequestDto
    {
        public int PlayerId { get; set; }
        public int ArmorId { get; set; }
        public int RoomId { get; set; }
        public int MerchantId { get; set; }
    }
}
