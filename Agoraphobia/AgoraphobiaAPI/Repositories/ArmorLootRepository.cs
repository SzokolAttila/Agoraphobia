using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ArmorLoot;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Armors;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class ArmorLootRepository : IArmorLootRepository
{
    private readonly ApplicationDBContext _context;

    public ArmorLootRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<ArmorLoot>> GetArmorLootsAsync(int roomId)
    {
        return await _context.ArmorLoots
            .Include(x => x.Armor)
            .Where(x => x.RoomId == roomId).ToListAsync();
    }

    public async Task<ArmorLoot> CreateAsync(ArmorLoot armorLoot)
    {
        await _context.ArmorLoots.AddAsync(armorLoot);
        await _context.SaveChangesAsync();
        return armorLoot;
    }

    public async Task<ArmorLoot?> GetByIdAsync(int id)
    {
        return await _context.ArmorLoots
            .Include(x => x.Armor)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<ArmorLoot?> AddOneAsync(int id)
    {
        var armorLoot = await _context.ArmorLoots.FirstOrDefaultAsync(x => x.Id == id);
        if (armorLoot is null)
            return null;
        
        armorLoot.Quantity += 1;
        await _context.SaveChangesAsync();
        return armorLoot;
    }

    public async Task<ArmorLoot?> DeleteAsync(int id)
    {
        var armorLootModel = _context.ArmorLoots.FirstOrDefault(x => x.Id == id);
        if (armorLootModel is null)
            return null;
        _context.ArmorLoots.Remove(armorLootModel);
        await _context.SaveChangesAsync();
        return armorLootModel;
    }

    public async Task<ArmorLoot?> RemoveOneAsync(int id)
    {
        var armorLoot = await _context.ArmorLoots.FirstOrDefaultAsync(x => x.Id == id);
        if (armorLoot is null)
            return null;
        
        armorLoot.Quantity -= 1;
        await _context.SaveChangesAsync();
        return armorLoot;
    }
}