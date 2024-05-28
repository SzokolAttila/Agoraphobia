namespace AgoraphobiaLibrary.JoinTables.Armors;

public class ArmorInventory
{
    public int PlayerId { get; set; }
    public int ArmorId { get; set; }
    public int Quantity { get; set; }
    public Player? Player { get; set; }
    public Armor? Armor { get; set; }
}