using AgoraphobiaAPI.Dtos.WeaponLoot;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponLootRepository
{
    public Task<List<WeaponLoot>> GetWeaponLootsAsync(int id);
    public Task<WeaponLoot> CreateAsync(WeaponLoot weaponLoot);
    public Task<WeaponLoot?> AddOneAsync(WeaponLootRequestDto update);
    public Task<WeaponLoot?> DeleteAsync(WeaponLoot weaponLoot);
    public Task<WeaponLoot?> RemoveOneAsync(WeaponLootRequestDto update);
}