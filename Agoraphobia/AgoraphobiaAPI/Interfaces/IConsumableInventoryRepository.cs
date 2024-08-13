using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces;

public interface IConsumableInventoryRepository
{
    public Task<List<ConsumableInventory>> GetConsumableInventoriesAsync(int id);
    public Task<ConsumableInventory> CreateAsync(ConsumableInventory consumableInventory);
    public Task<ConsumableInventory?> AddOneAsync(ConsumableInventoryRequestDto update);
    public Task<ConsumableInventory?> GetByIdAsync(int id);
    public Task<ConsumableInventory?> DeleteAsync(ConsumableInventory consumableInventory);
    public Task<ConsumableInventory?> RemoveOneAsync(ConsumableInventoryRequestDto update);}