using AgoraphobiaAPI.Dtos.WeaponDroprate;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Mappers;

public static class WeaponDroprateMapper
{
    public static WeaponDroprateDto ToWeaponDroprateDto(this WeaponDroprate weaponDroprate)
    {
        return new WeaponDroprateDto
        {
            Weapon = weaponDroprate.Weapon!.ToWeaponDto(),
            Droprate = weaponDroprate.Droprate,
            EnemyId = weaponDroprate.EnemyId,
            Id = weaponDroprate.Id,
            WeaponId = weaponDroprate.WeaponId,
        };
    }

    public static WeaponDroprate ToWeaponDroprate(this WeaponDroprateDto weaponDroprateDto)
    {
        return new WeaponDroprate
        {
            Weapon = weaponDroprateDto.Weapon.ToWeaponFromWeaponDto(),
            Droprate = weaponDroprateDto.Droprate
        };
    }

    public static UpdateWeaponDroprateRequestDto ToUpdateWeaponDroprateRequestDto(this WeaponDroprate weaponDroprate)
    {
        return new UpdateWeaponDroprateRequestDto
        {
            EnemyId = weaponDroprate.EnemyId,
            Weapon = weaponDroprate.Weapon!.ToWeaponDto(),
            Droprate = weaponDroprate.Droprate
        };
    }
}