using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaLibrary.JoinTables.Armors;

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

    public static UpdateArmorInventoryRequestDto ToUpdateArmorInventoryRequestDto(this ArmorInventory armorInventory)
    {
        return new UpdateArmorInventoryRequestDto
        {
            PlayerId = armorInventory.PlayerId,
            Armor = armorInventory.Armor!.ToArmorDto(),
            Quantity = armorInventory.Quantity
        };
    }
}