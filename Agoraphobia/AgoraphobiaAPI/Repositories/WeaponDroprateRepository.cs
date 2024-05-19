using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.WeaponDroprate;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
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

    public async Task<WeaponDroprate?> DeleteAsync(WeaponDroprate weaponDroprate)
    {
        var weaponDroprateModel = _context.WeaponDroprates.FirstOrDefault(
            x => x.EnemyId == weaponDroprate.EnemyId && x.WeaponId == weaponDroprate.WeaponId);
        if (weaponDroprateModel is null)
            return null;
        _context.WeaponDroprates.Remove(weaponDroprateModel);
        await _context.SaveChangesAsync();
        return weaponDroprateModel;
    }
}