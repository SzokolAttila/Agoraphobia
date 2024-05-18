using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class ConsumableInventoryRepository : IConsumableInventoryRepository
{
    private readonly ApplicationDBContext _context;

    public ConsumableInventoryRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<ConsumableInventory>> GetConsumableInventoriesAsync(int playerId)
    {
        return await _context.ConsumableInventories.Where(x => x.PlayerId == playerId).ToListAsync();
    }

    public async Task<ConsumableInventory> CreateAsync(ConsumableInventory consumableInventory)
    {
        await _context.ConsumableInventories.AddAsync(consumableInventory);
        await _context.SaveChangesAsync();
        return consumableInventory;
    }

    public async Task<ConsumableInventory?> AddOneAsync(ConsumableInventoryRequestDto update)
    {
        var consumableInventory = await _context.ConsumableInventories.FirstOrDefaultAsync(
            x => x.ConsumableId == update.ConsumableId && x.PlayerId == update.PlayerId);
        if (consumableInventory is null)
            return null;
        
        consumableInventory.Quantity += 1;
        await _context.SaveChangesAsync();
        return consumableInventory;
    }
}