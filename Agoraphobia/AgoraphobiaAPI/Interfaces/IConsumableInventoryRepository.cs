using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces;

public interface IConsumableInventoryRepository
{
    public Task<List<ConsumableInventory>> GetConsumableInventoriesAsync(int playerId);
    public Task<ConsumableInventory> CreateAsync(ConsumableInventory consumableInventory);
    public Task<ConsumableInventory?> AddOneAsync(int id);
    public Task<ConsumableInventory?> GetByIdAsync(int id);
    public Task<ConsumableInventory?> DeleteAsync(int id);
    public Task<ConsumableInventory?> RemoveOneAsync(int id);}