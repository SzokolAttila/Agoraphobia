using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Consumables;
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

    public async Task<ConsumableInventory?> AddOneAsync(int id)
    {
        var consumableInventory = await _context.ConsumableInventories
            .FirstOrDefaultAsync(x => x.ConsumableId == id);
        if (consumableInventory is null)
            return null;
        
        consumableInventory.Quantity += 1;
        await _context.SaveChangesAsync();
        return consumableInventory;
    }

    public async Task<ConsumableInventory?> GetByIdAsync(int id)
    {
        return await _context.ConsumableInventories
            .Include(x => x.Consumable)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<ConsumableInventory?> DeleteAsync(int id)
    {
        var consumableInventoryModel = _context.ConsumableInventories
            .FirstOrDefault(x => x.Id == id);
        if (consumableInventoryModel is null)
            return null;
        _context.ConsumableInventories.Remove(consumableInventoryModel);
        await _context.SaveChangesAsync();
        return consumableInventoryModel;
    }

    public async Task<ConsumableInventory?> RemoveOneAsync(int id)
    {
        var consumableInventory = await _context.ConsumableInventories
            .FirstOrDefaultAsync(x => x.ConsumableId == id);
        if (consumableInventory is null)
            return null;
        
        consumableInventory.Quantity -= 1;
        await _context.SaveChangesAsync();
        return consumableInventory;
    }
}