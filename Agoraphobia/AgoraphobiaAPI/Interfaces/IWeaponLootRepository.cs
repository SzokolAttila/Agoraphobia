using AgoraphobiaAPI.Dtos.WeaponLoot;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponLootRepository
{
    public Task<List<WeaponLoot>> GetWeaponLootsAsync(int id);
    public Task<WeaponLoot> CreateAsync(WeaponLoot weaponLoot);
    public Task<WeaponLoot?> AddOneAsync(int id);
    public Task<WeaponLoot?> GetByIdAsync(int id);
    public Task<WeaponLoot?> DeleteAsync(int id);
    public Task<WeaponLoot?> RemoveOneAsync(int id);
}