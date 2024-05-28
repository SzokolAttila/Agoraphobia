using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ConsumableDroprate;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class ConsumableDroprateRepository : IConsumableDroprateRepository
{
    private readonly ApplicationDBContext _context;
    public ConsumableDroprateRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<ConsumableDroprate>> GetConsumableDropratesAsync(int enemyId)
    {
        return await _context.ConsumableDroprates.Where(x => x.EnemyId == enemyId).ToListAsync();
    }

    public async Task<ConsumableDroprate> CreateAsync(ConsumableDroprate consumableDroprate)
    {
        await _context.ConsumableDroprates.AddAsync(consumableDroprate);
        await _context.SaveChangesAsync();
        return consumableDroprate;
    }

    public async Task<ConsumableDroprate?> DeleteAsync(ConsumableDroprate consumableDroprate)
    {
        var consumableDroprateModel = _context.ConsumableDroprates.FirstOrDefault(
            x => x.EnemyId == consumableDroprate.EnemyId && x.ConsumableId == consumableDroprate.ConsumableId);
        if (consumableDroprateModel is null)
            return null;
        _context.ConsumableDroprates.Remove(consumableDroprateModel);
        await _context.SaveChangesAsync();
        return consumableDroprateModel;
    }
}