using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaAPI.Dtos.ArmorSale;
using AgoraphobiaLibrary.JoinTables.Armors;

namespace AgoraphobiaAPI.Interfaces;

public interface IArmorInventoryRepository
{
    public Task<List<ArmorInventory>> GetArmorInventoriesAsync(int playerId);
    public Task<ArmorInventory> CreateAsync(ArmorInventory armorInventory);
    public Task<ArmorInventory?> DeleteAsync(int id);
    public Task<ArmorInventory?> GetByIdAsync(int id);
    public Task<ArmorInventory?> AddOneAsync(int id);
    public Task<ArmorInventory?> RemoveOneAsync(int id);
}