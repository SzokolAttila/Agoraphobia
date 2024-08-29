using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ArmorDroprate;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Armors;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class ArmorDroprateRepository : IArmorDroprateRepository
{
    private readonly ApplicationDBContext _context;
    public ArmorDroprateRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<ArmorDroprate>> GetArmorDropratesAsync(int enemyId)
    {
        return await _context.ArmorDroprates.Where(x => x.EnemyId == enemyId).ToListAsync();
    }

    public async Task<ArmorDroprate> CreateAsync(ArmorDroprate armorDroprate)
    {
        await _context.ArmorDroprates.AddAsync(armorDroprate);
        await _context.SaveChangesAsync();
        return armorDroprate;
    }

    public async Task<ArmorDroprate?> GetByIdAsync(int id)
    {
        return await _context.ArmorDroprates
            .Include(x => x.Armor)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<ArmorDroprate?> DeleteAsync(int id)
    {
        var armorDroprateModel = _context.ArmorDroprates.FirstOrDefault(x => x.Id == id);
        if (armorDroprateModel is null)
            return null;
        _context.ArmorDroprates.Remove(armorDroprateModel);
        await _context.SaveChangesAsync();
        return armorDroprateModel;
    }
}