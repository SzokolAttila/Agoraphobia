using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponInventory;

public class UpdateWeaponInventoryRequestDto
{
    public int PlayerId { get; set; }
    public WeaponDto Weapon { get; set; } = new();
    public int Quantity { get; set; }
}