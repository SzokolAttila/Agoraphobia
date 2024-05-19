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

    public static UpdateWeaponInventoryRequestDto ToUpdateWeaponInventoryRequestDto(this WeaponInventory weaponInventory)
    {
        return new UpdateWeaponInventoryRequestDto
        {
            Weapon = weaponInventory.Weapon!.ToWeaponDto(),
            PlayerId = weaponInventory.PlayerId,
            Quantity = weaponInventory.Quantity
        };
    }
}