using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponLoot;

public class WeaponLootDto
{
    public int Id { get; set; }
    public int WeaponId { get; set; }
    public int RoomId { get; set; }
    public WeaponDto Weapon { get; set; } = new();
    public int Quantity { get; set; }
}