using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Armors;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class ArmorInventoryRepository : IArmorInventoryRepository
{
    private readonly ApplicationDBContext _context;
    public ArmorInventoryRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<ArmorInventory>> GetArmorInventoriesAsync(int playerId)
    {
        return await _context.ArmorInventories.Where(x => x.PlayerId == playerId).ToListAsync();
    }

    public async Task<ArmorInventory> CreateAsync(ArmorInventory armorInventory)
    {
        await _context.ArmorInventories.AddAsync(armorInventory);
        await _context.SaveChangesAsync();
        return armorInventory;
    }

    public async Task<ArmorInventory?> GetByIdAsync(int id)
    {
        return await _context.ArmorInventories
            .Include(x => x.Armor)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ArmorInventory?> DeleteAsync(int id)
    {
        var armorInventoryModel = _context.ArmorInventories.FirstOrDefault(x => x.Id == id);
        if (armorInventoryModel is null)
            return null;
        _context.ArmorInventories.Remove(armorInventoryModel);
        await _context.SaveChangesAsync();
        return armorInventoryModel;
    }

    public async Task<ArmorInventory?> AddOneAsync(int id)
    {
        var armorInventory = await _context.ArmorInventories.FirstOrDefaultAsync(x => x.Id == id);
        if (armorInventory is null)
            return null;
        
        armorInventory.Quantity += 1;
        await _context.SaveChangesAsync();
        return armorInventory;
    }

    public async Task<ArmorInventory?> RemoveOneAsync(int id)
    {
        var armorInventory = await _context.ArmorInventories.FirstOrDefaultAsync(x => x.Id == id);
        if (armorInventory is null)
            return null;
        
        armorInventory.Quantity -= 1;
        await _context.SaveChangesAsync();
        return armorInventory;
    }
}