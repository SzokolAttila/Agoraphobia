using AgoraphobiaAPI.Dtos.Armor;

namespace AgoraphobiaAPI.Dtos.ArmorLoot;

public class UpdateArmorLootRequestDto
{
    public int RoomId { get; set; }
    public ArmorDto Armor { get; set; } = new();
    public int Quantity { get; set; }
}