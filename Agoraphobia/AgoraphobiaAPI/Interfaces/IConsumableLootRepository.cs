using AgoraphobiaAPI.Dtos.ConsumableLoot;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces;

public interface IConsumableLootRepository
{
    public Task<List<ConsumableLoot>> GetConsumableLootsAsync(int roomId);
    public Task<ConsumableLoot> CreateAsync(ConsumableLoot consumableLoot);
    public Task<ConsumableLoot?> AddOneAsync(int id);
    public Task<ConsumableLoot?> DeleteAsync(int id);
    public Task<ConsumableLoot?> GetByIdAsync(int id);
    public Task<ConsumableLoot?> RemoveOneAsync(int id);
}