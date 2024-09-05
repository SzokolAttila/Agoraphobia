namespace AgoraphobiaLibrary.JoinTables.Armors;

public class ArmorLoot
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int ArmorId { get; set; }
    public int Quantity { get; set; }
    public Room? Room { get; set; }
    public Armor? Armor { get; set; }
}