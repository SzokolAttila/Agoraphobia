using AgoraphobiaAPI.Dtos.ArmorLoot;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class ArmorLootMapper
{
    public static ArmorLootDto ToArmorLootDto(this ArmorLoot armorLoot)
    {
        return new ArmorLootDto
        {
            Quantity = armorLoot.Quantity,
            Armor = armorLoot.Armor!.ToArmorDto()
        };
    }

    public static UpdateArmorLootRequestDto ToUpdateArmorLootRequestDto(this ArmorLoot armorLoot)
    {
        return new UpdateArmorLootRequestDto
        {
            Armor = armorLoot.Armor!.ToArmorDto(),
            RoomId = armorLoot.RoomId,
            Quantity = armorLoot.Quantity
        };
    }
}