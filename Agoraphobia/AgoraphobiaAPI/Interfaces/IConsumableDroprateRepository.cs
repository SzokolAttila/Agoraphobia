using AgoraphobiaAPI.Dtos.ConsumableDroprate;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IConsumableDroprateRepository
{
    public Task<List<ConsumableDroprate>> GetConsumableDropratesAsync(int id);
    public Task<ConsumableDroprate> CreateAsync(ConsumableDroprate consumableDroprate);
    public Task<ConsumableDroprate?> DeleteAsync(ConsumableDroprate consumableDroprate);
}