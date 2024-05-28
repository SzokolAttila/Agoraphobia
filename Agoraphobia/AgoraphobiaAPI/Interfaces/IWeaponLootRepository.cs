using AgoraphobiaAPI.Dtos.WeaponLoot;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponLootRepository
{
    public Task<List<WeaponLoot>> GetWeaponLootsAsync(int id);
    public Task<WeaponLoot> CreateAsync(WeaponLoot weaponLoot);
    public Task<WeaponLoot?> AddOneAsync(WeaponLootRequestDto update);
    public Task<WeaponLoot?> DeleteAsync(WeaponLoot weaponLoot);
    public Task<WeaponLoot?> RemoveOneAsync(WeaponLootRequestDto update);
}