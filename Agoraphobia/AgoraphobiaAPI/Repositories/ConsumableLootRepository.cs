using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ConsumableLoot;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
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
        return await _context.ConsumableLoots.Where(x => x.RoomId == roomId).ToListAsync();
    }

    public async Task<ConsumableLoot> CreateAsync(ConsumableLoot consumableLoot)
    {
        await _context.ConsumableLoots.AddAsync(consumableLoot);
        await _context.SaveChangesAsync();
        return consumableLoot;
    }

    public async Task<ConsumableLoot?> AddOneAsync(ConsumableLootRequestDto update)
    {
        var consumableLoot = await _context.ConsumableLoots.FirstOrDefaultAsync(
            x => x.ConsumableId == update.ConsumableId && x.RoomId == update.RoomId);
        if (consumableLoot is null)
            return null;
        
        consumableLoot.Quantity += 1;
        await _context.SaveChangesAsync();
        return consumableLoot;
    }

    public async Task<ConsumableLoot?> DeleteAsync(ConsumableLoot consumableLoot)
    {
        var consumableLootModel = _context.ConsumableLoots.FirstOrDefault(
            x => x.RoomId == consumableLoot.RoomId && x.ConsumableId == consumableLoot.ConsumableId);
        if (consumableLootModel is null)
            return null;
        _context.ConsumableLoots.Remove(consumableLootModel);
        await _context.SaveChangesAsync();
        return consumableLootModel;
    }

    public async Task<ConsumableLoot?> RemoveOneAsync(ConsumableLootRequestDto update)
    {
        var consumableLoot = await _context.ConsumableLoots.FirstOrDefaultAsync(
            x => x.ConsumableId == update.ConsumableId && x.RoomId == update.RoomId);
        if (consumableLoot is null)
            return null;
        
        consumableLoot.Quantity -= 1;
        await _context.SaveChangesAsync();
        return consumableLoot;
    }
}