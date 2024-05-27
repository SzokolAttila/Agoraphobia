using AgoraphobiaAPI.Dtos.WeaponLoot;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class WeaponLootMapper
{
    public static WeaponLootDto ToWeaponLootDto(this WeaponLoot weaponLoot)
    {
        return new WeaponLootDto
        {
            Quantity = weaponLoot.Quantity,
            Weapon = weaponLoot.Weapon!.ToWeaponDto()
        };
    }

    public static UpdateWeaponLootRequestDto ToUpdateWeaponLootRequestDto(this WeaponLoot weaponLoot)
    {
        return new UpdateWeaponLootRequestDto
        {
            Weapon = weaponLoot.Weapon!.ToWeaponDto(),
            RoomId = weaponLoot.RoomId,
            Quantity = weaponLoot.Quantity
        };
    }
}