using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaAPI.Dtos.Room;

namespace AgoraphobiaAPI.Dtos.RoomMerchantConsumableSaleStatus
{
    public class RoomMerchantConsumableSaleStatusDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int RoomId { get; set; }
        public int MerchantId { get; set; }
        public int ConsumableId { get; set; }
        public ConsumableDto Consumable { get; set; } = new();
        public int Quantity { get; set; }
    }
}
