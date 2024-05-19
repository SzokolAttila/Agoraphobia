using AgoraphobiaAPI.Dtos.Weapon;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class WeaponMapper
{
    public static Weapon ToWeaponFromCreateDto(this CreateWeaponRequestDto weapon)
    {
        return new Weapon(weapon.Name, weapon.Description, weapon.RarityIdx, weapon.Price, weapon.MinMultiplier, weapon.MaxMultiplier, weapon.Energy);
    }

    public static Weapon ToWeaponFromWeaponDto(this WeaponDto weaponDto)
    {
        return new Weapon(weaponDto.Id, weaponDto.Name, weaponDto.Description, weaponDto.RarityIdx, weaponDto.Price, weaponDto.MinMultiplier, weaponDto.MaxMultiplier, weaponDto.Price);
    }

    public static WeaponDto ToWeaponDto(this Weapon weapon)
    {
        return new WeaponDto
        {
            Id = weapon.Id,
            Name = weapon.Name,
            Description = weapon.Description,
            RarityIdx = weapon.RarityIdx,
            Price = weapon.Price,
            MinMultiplier = weapon.MinMultiplier,
            MaxMultiplier = weapon.MaxMultiplier,
            Energy = weapon.Energy
        };
    }
}