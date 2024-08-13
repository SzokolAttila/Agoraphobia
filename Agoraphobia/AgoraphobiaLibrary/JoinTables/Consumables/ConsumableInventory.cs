namespace AgoraphobiaLibrary.JoinTables.Consumables;

public class ConsumableInventory
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int ConsumableId { get; set; }
    public int Quantity { get; set; }
    public Player? Player { get; set; }
    public Consumable? Consumable { get; set; }
}