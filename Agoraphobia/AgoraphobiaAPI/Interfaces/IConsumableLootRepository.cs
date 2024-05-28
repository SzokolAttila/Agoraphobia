using AgoraphobiaAPI.Dtos.ConsumableLoot;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces;

public interface IConsumableLootRepository
{
    public Task<List<ConsumableLoot>> GetConsumableLootsAsync(int id);
    public Task<ConsumableLoot> CreateAsync(ConsumableLoot consumableLoot);
    public Task<ConsumableLoot?> AddOneAsync(ConsumableLootRequestDto update);
    public Task<ConsumableLoot?> DeleteAsync(ConsumableLoot consumableLoot);
    public Task<ConsumableLoot?> RemoveOneAsync(ConsumableLootRequestDto update);
}