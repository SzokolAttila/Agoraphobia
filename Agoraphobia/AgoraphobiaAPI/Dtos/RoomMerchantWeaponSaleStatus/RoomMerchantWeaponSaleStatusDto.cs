using AgoraphobiaAPI.Dtos.Weapon;
using AgoraphobiaAPI.Dtos.Room;

namespace AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus
{
    public class RoomMerchantWeaponSaleStatusDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int WeaponId { get; set; }
        public int PlayerId { get; set; }
        public int MerchantId { get; set; }
        public WeaponDto Weapon { get; set; } = new();
        public int Quantity { get; set; }
    }
}
