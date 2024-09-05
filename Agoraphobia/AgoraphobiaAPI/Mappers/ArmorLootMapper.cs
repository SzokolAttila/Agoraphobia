using AgoraphobiaAPI.Dtos.ArmorLoot;
using AgoraphobiaLibrary.JoinTables.Armors;

namespace AgoraphobiaAPI.Mappers;

public static class ArmorLootMapper
{
    public static ArmorLootDto ToArmorLootDto(this ArmorLoot armorLoot)
    {
        return new ArmorLootDto
        {
            Quantity = armorLoot.Quantity,
            Armor = armorLoot.Armor!.ToArmorDto(),
            RoomId = armorLoot.RoomId,
            ArmorId = armorLoot.ArmorId,
            Id = armorLoot.Id,
        };
    }
}