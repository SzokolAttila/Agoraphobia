using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ConsumableLoot;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class ConsumableLootRepository : IConsumableLootRepository
{
    private readonly ApplicationDBContext _context;

    public ConsumableLootRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<ConsumableLoot>> GetConsumableLootsAsync(int roomId)
    {
        return await _context.ConsumableLoots
            .Include(x => x.Consumable)
            .Where(x => x.RoomId == roomId).ToListAsync();
    }

    public async Task<ConsumableLoot> CreateAsync(ConsumableLoot consumableLoot)
    {
        await _context.ConsumableLoots.AddAsync(consumableLoot);
        await _context.SaveChangesAsync();
        return consumableLoot;
    }

    public async Task<ConsumableLoot?> GetByIdAsync(int id)
    {
        return await _context.ConsumableLoots
            .Include(x => x.Consumable)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ConsumableLoot?> AddOneAsync(int id)
    {
        var consumableLoot = await _context.ConsumableLoots.FirstOrDefaultAsync(x => x.Id == id);
        if (consumableLoot is null)
            return null;
        
        consumableLoot.Quantity += 1;
        await _context.SaveChangesAsync();
        return consumableLoot;
    }

    public async Task<ConsumableLoot?> DeleteAsync(int id)
    {
        var consumableLootModel = _context.ConsumableLoots.FirstOrDefault(x => x.Id == id);
        if (consumableLootModel is null)
            return null;
        _context.ConsumableLoots.Remove(consumableLootModel);
        await _context.SaveChangesAsync();
        return consumableLootModel;
    }

    public async Task<ConsumableLoot?> RemoveOneAsync(int id)
    {
        var consumableLoot = await _context.ConsumableLoots.FirstOrDefaultAsync(x => x.Id == id);
        if (consumableLoot is null)
            return null;
        
        consumableLoot.Quantity -= 1;
        await _context.SaveChangesAsync();
        return consumableLoot;
    }
}