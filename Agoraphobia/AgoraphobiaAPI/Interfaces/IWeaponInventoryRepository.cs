using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponInventoryRepository
{
    public Task<List<WeaponInventory>> GetWeaponInventoriesAsync(int id);
    public Task<WeaponInventory> CreateAsync(WeaponInventory weaponInventory);
    public Task<WeaponInventory?> AddOneAsync(WeaponInventoryRequestDto update);
    public Task<WeaponInventory?> DeleteAsync(WeaponInventory weaponInventory);
    public Task<WeaponInventory?> RemoveOneAsync(WeaponInventoryRequestDto update);
}