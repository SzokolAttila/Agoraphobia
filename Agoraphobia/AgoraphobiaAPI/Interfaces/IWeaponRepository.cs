using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgoraphobiaAPI.Dtos.Weapon;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IWeaponRepository
    {
        Task<List<Weapon>> GetAllAsync();
        Task<Weapon?> GetByIdAsync(int id);
        Task<Weapon> CreateAsync(Weapon weaponModel);
        Task<Weapon?> UpdateAsync(int id, CreateWeaponRequestDto weaponDto);
        Task<Weapon?> DeleteAsync(int id);
    }
}