using Newtonsoft.Json;

namespace AgoraphobiaLibrary.JoinTables.Weapons;

public class WeaponInventory
{
    public int PlayerId { get; set; }
    public int WeaponId { get; set; }
    public int Quantity { get; set; }
    public Player? Player { get; set; }
    public Weapon? Weapon { get; set; }
}