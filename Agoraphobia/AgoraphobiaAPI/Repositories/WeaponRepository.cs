using System.Xml;
using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Weapon;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;
using static AgoraphobiaLibrary.Item;

namespace AgoraphobiaAPI.Repositories;

public class WeaponRepository : IWeaponRepository
{
    private readonly ApplicationDBContext _context;
    public WeaponRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Weapon>> GetAllAsync()
    {
        return await _context.Weapons.ToListAsync();
    }

    public async Task<Weapon?> GetByIdAsync(int id)
    {
        return await _context.Weapons.FindAsync(id);
    }

    public async Task<Weapon> CreateAsync(Weapon weaponModel)
    {
        await _context.Weapons.AddAsync(weaponModel);
        await _context.SaveChangesAsync();
        return weaponModel;
    }

    public async Task<Weapon?> UpdateAsync(int id, CreateWeaponRequestDto weaponDto)
    {
        var weaponModel = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
        if (weaponModel is null)
            return null;
        
        weaponModel.Name = weaponDto.Name;
        weaponModel.Description = weaponDto.Description;
        weaponModel.Rarity = (ItemRarity)weaponDto.RarityIdx;
        weaponModel.Price = weaponDto.Price;
        weaponModel.MinMultiplier = weaponDto.MinMultiplier;
        weaponModel.MaxMultiplier = weaponDto.MaxMultiplier;
        weaponModel.Energy = weaponDto.Energy;
        await _context.SaveChangesAsync();
        return weaponModel;
    }

    public async Task<Weapon?> DeleteAsync(int id)
    {
        var weaponModel = await _context.Weapons.FirstOrDefaultAsync(x => x.Id == id);
        if (weaponModel is null)
            return null;
        _context.Weapons.Remove(weaponModel);
        await _context.SaveChangesAsync();
        return weaponModel;
    }
}