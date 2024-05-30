using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.RoomWeaponLootStatus
{
    public class RoomWeaponLootStatusDto
    {
        public int PlayerId { get; set; }
        public RoomDto Room { get; set; } = new();
        public WeaponDto Weapon { get; set; } = new();
        public int Quantity { get; set; }
    }
}
