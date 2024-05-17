using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class ArmorInventoryMapper
{
    public static ArmorInventoryDto ToArmorInventoryDto(this ArmorInventory armorInventory)
    {
        return new ArmorInventoryDto
        {
            Armor = armorInventory.Armor!.ToArmorDto(),
            Quantity = armorInventory.Quantity
        };
    }
}