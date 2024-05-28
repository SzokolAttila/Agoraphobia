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

    public async Task<ArmorDroprate?> DeleteAsync(ArmorDroprate armorDroprate)
    {
        var armorDroprateModel = _context.ArmorDroprates.FirstOrDefault(
            x => x.EnemyId == armorDroprate.EnemyId && x.ArmorId == armorDroprate.ArmorId);
        if (armorDroprateModel is null)
            return null;
        _context.ArmorDroprates.Remove(armorDroprateModel);
        await _context.SaveChangesAsync();
        return armorDroprateModel;
    }
}