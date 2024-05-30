namespace AgoraphobiaAPI.Dtos.RoomMerchantConsumableSaleStatus
{
    public class ConsumableSaleStatusRequestDto
    {
        public int PlayerId { get; set; }
        public int ConsumableId { get; set; }
        public int RoomId { get; set; }
        public int MerchantId { get; set; }
    }
}
