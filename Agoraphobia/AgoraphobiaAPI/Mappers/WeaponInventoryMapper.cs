using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class WeaponInventoryMapper
{
    public static WeaponInventoryDto ToWeaponInventoryDto(this WeaponInventory weaponInventory)
    {
        return new WeaponInventoryDto
        {
            Quantity = weaponInventory.Quantity,
            Weapon = weaponInventory.Weapon!.ToWeaponDto()
        };
    }
}