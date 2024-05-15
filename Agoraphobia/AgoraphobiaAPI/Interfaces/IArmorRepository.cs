using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IArmorRepository
    {
        Task<List<Armor>> GetAllAsync();
        Task<Armor?> GetByIdAsync(int id);
        Task<Armor> CreateAsync(Armor armorModel);
        Task<Armor?> UpdateAsync(int id, CreateArmorRequestDto armorDto);
        Task<Armor?> DeleteAsync(int id);
    }
}