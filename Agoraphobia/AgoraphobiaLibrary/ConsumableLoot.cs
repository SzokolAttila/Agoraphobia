namespace AgoraphobiaLibrary;

public class ConsumableLoot
{
    public int RoomId { get; set; }
    public int ConsumableId { get; set; }
    public int Quantity { get; set; }
    public Room? Room { get; set; }
    public Consumable? Consumable { get; set; }
}