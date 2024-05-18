using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IWeaponInventoryRepository
{
    public Task<List<WeaponInventory>> GetWeaponInventoriesAsync(int id);

}