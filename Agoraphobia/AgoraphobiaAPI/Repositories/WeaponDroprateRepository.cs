using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.WeaponDroprate;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class WeaponDroprateRepository : IWeaponDroprateRepository
{
    private readonly ApplicationDBContext _context;
    public WeaponDroprateRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<WeaponDroprate>> GetWeaponDropratesAsync(int enemyId)
    {
        return await _context.WeaponDroprates.Where(x => x.EnemyId == enemyId).ToListAsync();
    }

    public async Task<WeaponDroprate> CreateAsync(WeaponDroprate weaponDroprate)
    {
        await _context.WeaponDroprates.AddAsync(weaponDroprate);
        await _context.SaveChangesAsync();
        return weaponDroprate;
    }

    public async Task<WeaponDroprate?> DeleteAsync(int id)
    {
        var weaponDroprateModel = _context.WeaponDroprates.FirstOrDefault(x => x.Id == id);
        if (weaponDroprateModel is null)
            return null;
        _context.WeaponDroprates.Remove(weaponDroprateModel);
        await _context.SaveChangesAsync();
        return weaponDroprateModel;
    }

    public async Task<WeaponDroprate?> GetByIdAsync(int id)
    {
        return await _context.WeaponDroprates
            .Include(x => x.Weapon)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}