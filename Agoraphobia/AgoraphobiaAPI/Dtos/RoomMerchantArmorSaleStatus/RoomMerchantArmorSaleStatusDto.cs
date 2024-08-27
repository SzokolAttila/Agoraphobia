using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaAPI.Dtos.Room;

namespace AgoraphobiaAPI.Dtos.RoomMerchantArmorSaleStatus
{
    public class RoomMerchantArmorSaleStatusDto
    {
        public int Id { get; set; }
        public int ArmorId { get; set; }
        public int RoomId { get; set; }
        public int MerchantId { get; set; }
        public int PlayerId { get; set; }
        public ArmorDto Armor { get; set; } = new();
        public int Quantity { get; set; }
    }
}
