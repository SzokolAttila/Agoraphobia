using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IConsumableRepository
    {
        Task<List<Consumable>> GetAllAsync();
        Task<Consumable?> GetByIdAsync(int id);
        Task<Consumable> CreateAsync(Consumable consumableModel);
        Task<Consumable?> UpdateAsync(int id, CreateConsumableRequestDto consumableDto);
        Task<Consumable?> DeleteAsync(int id);
    }
}