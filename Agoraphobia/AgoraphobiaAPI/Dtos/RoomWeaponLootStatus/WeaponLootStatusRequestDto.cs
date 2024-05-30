namespace AgoraphobiaAPI.Dtos.RoomWeaponLootStatus
{
    public class WeaponLootStatusRequestDto
    {
        public int PlayerId { get; set; }
        public int RoomId { get; set; }
        public int WeaponId { get; set; }
    }
}
