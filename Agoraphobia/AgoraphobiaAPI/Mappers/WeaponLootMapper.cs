using AgoraphobiaAPI.Dtos.WeaponLoot;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Mappers;

public static class WeaponLootMapper
{
    public static WeaponLootDto ToWeaponLootDto(this WeaponLoot weaponLoot)
    {
        return new WeaponLootDto
        {
            Quantity = weaponLoot.Quantity,
            Weapon = weaponLoot.Weapon!.ToWeaponDto(),
            RoomId = weaponLoot.RoomId,
            WeaponId = weaponLoot.WeaponId,
            Id = weaponLoot.Id
        };
    }
}