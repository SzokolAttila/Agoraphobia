using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.RoomWeaponLootStatus
{
    public class RoomWeaponLootStatusDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int RoomId { get; set; } 
        public int WeaponId { get; set; }
        public WeaponDto Weapon { get; set; } = new();
        public int Quantity { get; set; }
    }
}
