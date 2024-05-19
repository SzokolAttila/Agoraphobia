using AgoraphobiaAPI.Dtos.WeaponDroprate;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponDroprateRepository
{
    public Task<List<WeaponDroprate>> GetWeaponDropratesAsync(int id);
    public Task<WeaponDroprate> CreateAsync(WeaponDroprate weaponDroprate);
    public Task<WeaponDroprate?> DeleteAsync(WeaponDroprate weaponDroprate);
}