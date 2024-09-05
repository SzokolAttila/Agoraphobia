using AgoraphobiaAPI.Dtos.WeaponDroprate;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponDroprateRepository
{
    public Task<List<WeaponDroprate>> GetWeaponDropratesAsync(int enemyId);
    public Task<WeaponDroprate> CreateAsync(WeaponDroprate weaponDroprate);
    public Task<WeaponDroprate?> GetByIdAsync(int id);
    public Task<WeaponDroprate?> DeleteAsync(int id);
}