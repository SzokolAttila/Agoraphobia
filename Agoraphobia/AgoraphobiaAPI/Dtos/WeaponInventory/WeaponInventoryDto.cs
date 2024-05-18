using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponInventory;

public class WeaponInventoryDto
{
    public WeaponDto Weapon { get; set; } = new();
    public int Quantity { get; set; }
}