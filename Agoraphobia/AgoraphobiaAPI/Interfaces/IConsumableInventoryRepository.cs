using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IConsumableInventoryRepository
{
    public Task<List<ConsumableInventory>> GetConsumableInventoriesAsync(int id);
    public Task<ConsumableInventory> CreateAsync(ConsumableInventory consumableInventory);
    public Task<ConsumableInventory?> AddOneAsync(ConsumableInventoryRequestDto update);
}