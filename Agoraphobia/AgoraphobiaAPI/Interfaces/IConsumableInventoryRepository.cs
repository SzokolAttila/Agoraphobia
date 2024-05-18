using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IConsumableInventoryRepository
{
    public Task<List<ConsumableInventory>> GetConsumableInventoriesAsync(int id);

}