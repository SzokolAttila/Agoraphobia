using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
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

    public async Task<ArmorInventory?> AddOneAsync(CreateArmorInventoryRequestDto update)
    {
        var armorInventory = await _context.ArmorInventories.FirstOrDefaultAsync(
            x => x.ArmorId == update.ArmorId && x.PlayerId == update.PlayerId);
        if (armorInventory is null)
            return null;
        
        armorInventory.Quantity += 1;
        await _context.SaveChangesAsync();
        return armorInventory;
    }
}