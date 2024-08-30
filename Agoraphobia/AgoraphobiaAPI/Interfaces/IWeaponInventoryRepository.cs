using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponInventoryRepository
{
    public Task<List<WeaponInventory>> GetWeaponInventoriesAsync(int playerId);
    public Task<WeaponInventory> CreateAsync(WeaponInventory weaponInventory);
    public Task<WeaponInventory?> AddOneAsync(int id);
    public Task<WeaponInventory?> GetByIdAsync(int id);
    public Task<WeaponInventory?> DeleteAsync(int id);
    public Task<WeaponInventory?> RemoveOneAsync(int id);
}