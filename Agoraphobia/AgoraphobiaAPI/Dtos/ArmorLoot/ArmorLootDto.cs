using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorLoot;

public class ArmorLootDto
{
    public int ArmorId { get; set; }
    public ArmorDto Armor { get; set; } = new();
    public int RoomId { get; set; }
    public int Quantity { get; set; }
}