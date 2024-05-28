namespace AgoraphobiaLibrary.JoinTables.Weapons;

public class WeaponLoot
{
    public int RoomId { get; set; }
    public int WeaponId { get; set; }
    public int Quantity { get; set; }
    public Room? Room { get; set; }
    public Weapon? Weapon { get; set; }
}