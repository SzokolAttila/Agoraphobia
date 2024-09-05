using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.WeaponLoot;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class WeaponLootRepository : IWeaponLootRepository
{
    private readonly ApplicationDBContext _context;

    public WeaponLootRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<WeaponLoot>> GetWeaponLootsAsync(int roomId)
    {
        return await _context.WeaponLoots
            .Include(x => x.Weapon)
            .Where(x => x.RoomId == roomId).ToListAsync();
    }

    public async Task<WeaponLoot> CreateAsync(WeaponLoot weaponLoot)
    {
        await _context.WeaponLoots.AddAsync(weaponLoot);
        await _context.SaveChangesAsync();
        return weaponLoot;
    }

    public async Task<WeaponLoot?> AddOneAsync(int id)
    {
        var weaponLoot = await _context.WeaponLoots.FirstOrDefaultAsync(x => x.Id == id);
        if (weaponLoot is null)
            return null;
        
        weaponLoot.Quantity += 1;
        await _context.SaveChangesAsync();
        return weaponLoot;
    }

    public async Task<WeaponLoot?> DeleteAsync(int id)
    {
        var weaponLootModel = _context.WeaponLoots.FirstOrDefault(x => x.Id == id);
        if (weaponLootModel is null)
            return null;
        _context.WeaponLoots.Remove(weaponLootModel);
        await _context.SaveChangesAsync();
        return weaponLootModel;
    }

    public async Task<WeaponLoot?> GetByIdAsync(int id)
    {
        return await _context.WeaponLoots
            .Include(x => x.Weapon)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<WeaponLoot?> RemoveOneAsync(int id)
    {
        var weaponLoot = await _context.WeaponLoots.FirstOrDefaultAsync(x => x.Id == id);
        if (weaponLoot is null)
            return null;
        
        weaponLoot.Quantity -= 1;
        await _context.SaveChangesAsync();
        return weaponLoot;
    }
}