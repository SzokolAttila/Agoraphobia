using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponInventoryRepository
{
    public Task<List<WeaponInventory>> GetWeaponInventoriesAsync(int id);
    public Task<WeaponInventory> CreateAsync(WeaponInventory weaponInventory);
    public Task<WeaponInventory?> AddOneAsync(WeaponInventoryRequestDto update);
}