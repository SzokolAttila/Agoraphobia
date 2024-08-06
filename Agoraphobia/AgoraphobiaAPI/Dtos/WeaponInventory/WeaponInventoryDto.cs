using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaAPI.Dtos.Weapon;

namespace AgoraphobiaAPI.Dtos.WeaponInventory;

public class WeaponInventoryDto
{
    public int WeaponId { get; set; }
    public WeaponDto? Weapon { get; set; }
    public int PlayerId { get; set; }
    public int Quantity { get; set; }
}