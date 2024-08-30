using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Mappers;

public static class WeaponInventoryMapper
{
    public static WeaponInventoryDto ToWeaponInventoryDto(this WeaponInventory weaponInventory)
    {
        return new WeaponInventoryDto
        {
            Id = weaponInventory.Id,
            Quantity = weaponInventory.Quantity,
            PlayerId = weaponInventory.PlayerId,
            Weapon = weaponInventory.Weapon!.ToWeaponDto(),
            WeaponId = weaponInventory.WeaponId,
        };
    }
}