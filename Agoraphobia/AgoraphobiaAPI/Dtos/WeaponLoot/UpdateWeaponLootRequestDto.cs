using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponLoot;

public class UpdateWeaponLootRequestDto
{
    public int RoomId { get; set; }
    public WeaponDto Weapon { get; set; } = new();
    public int Quantity { get; set; }
}