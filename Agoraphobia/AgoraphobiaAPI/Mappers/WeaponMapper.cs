using AgoraphobiaAPI.Dtos.Weapon;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class WeaponMapper
{
    public static Weapon ToWeaponFromCreateDto(this CreateWeaponRequestDto weapon)
    {
        return new Weapon(weapon.Name, weapon.Description, weapon.RarityIdx, weapon.Price, weapon.MinMultiplier, weapon.MaxMultiplier, weapon.Energy);
    }

    public static WeaponDto ToWeaponDto(this Weapon weapon)
    {
        return new WeaponDto
        {
            Id = weapon.Id,
            Description = weapon.Description,
            Energy = weapon.Energy,
            MaxMultiplier = weapon.MaxMultiplier,
            MinMultiplier = weapon.MinMultiplier,
            Name = weapon.Name,
            RarityIdx = weapon.RarityIdx,
            Price = weapon.Price,
        };
    }
}