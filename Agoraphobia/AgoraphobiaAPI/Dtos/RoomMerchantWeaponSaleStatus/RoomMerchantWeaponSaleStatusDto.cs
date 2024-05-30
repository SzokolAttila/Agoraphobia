using AgoraphobiaAPI.Dtos.Weapon;
using AgoraphobiaAPI.Dtos.Room;

namespace AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus
{
    public class RoomMerchantWeaponSaleStatusDto
    {
        public int PlayerId { get; set; }
        public RoomDto Room { get; set; } = new();
        public WeaponDto Weapon { get; set; } = new();
        public int Quantity { get; set; }
    }
}
