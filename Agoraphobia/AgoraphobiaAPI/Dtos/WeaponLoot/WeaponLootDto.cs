using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponLoot;

public class WeaponLootDto
{
    public WeaponDto Weapon { get; set; } = new();
    public int Quantity { get; set; }
}