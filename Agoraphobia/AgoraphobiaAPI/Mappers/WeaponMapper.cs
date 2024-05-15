using AgoraphobiaAPI.Dtos.Weapon;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class WeaponMapper
{
    public static Weapon ToWeaponFromCreateDto(this CreateWeaponRequestDto weapon)
    {
        return new Weapon(weapon.Name, weapon.Description, weapon.RarityIdx, weapon.Price, weapon.MinMultiplier, weapon.MaxMultiplier, weapon.Energy);
    }
}