using AgoraphobiaAPI.Dtos.ConsumableDroprate;
using AgoraphobiaLibrary.JoinTables.Consumables;

namespace AgoraphobiaAPI.Interfaces;

public interface IConsumableDroprateRepository
{
    public Task<List<ConsumableDroprate>> GetConsumableDropratesAsync(int id);
    public Task<ConsumableDroprate> CreateAsync(ConsumableDroprate consumableDroprate);
    public Task<ConsumableDroprate?> GetByIdAsync(int id);
    public Task<ConsumableDroprate?> DeleteAsync(int id);
}