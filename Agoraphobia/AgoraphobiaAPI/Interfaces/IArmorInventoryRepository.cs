using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaLibrary.JoinTables.Armors;

namespace AgoraphobiaAPI.Interfaces;

public interface IArmorInventoryRepository
{
    public Task<List<ArmorInventory>> GetArmorInventoriesAsync(int id);
    public Task<ArmorInventory> CreateAsync(ArmorInventory armorInventory);
    public Task<ArmorInventory?> DeleteAsync(ArmorInventory armorInventory);
    public Task<ArmorInventory?> AddOneAsync(ArmorInventoryRequestDto update);
    public Task<ArmorInventory?> RemoveOneAsync(ArmorInventoryRequestDto update);
}