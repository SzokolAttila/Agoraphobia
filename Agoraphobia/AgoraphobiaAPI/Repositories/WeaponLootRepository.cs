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
        return await _context.WeaponLoots.Where(x => x.RoomId == roomId).ToListAsync();
    }

    public async Task<WeaponLoot> CreateAsync(WeaponLoot weaponLoot)
    {
        await _context.WeaponLoots.AddAsync(weaponLoot);
        await _context.SaveChangesAsync();
        return weaponLoot;
    }

    public async Task<WeaponLoot?> AddOneAsync(WeaponLootRequestDto update)
    {
        var weaponLoot = await _context.WeaponLoots.FirstOrDefaultAsync(
            x => x.WeaponId == update.WeaponId && x.RoomId == update.RoomId);
        if (weaponLoot is null)
            return null;
        
        weaponLoot.Quantity += 1;
        await _context.SaveChangesAsync();
        return weaponLoot;
    }

    public async Task<WeaponLoot?> DeleteAsync(WeaponLoot weaponLoot)
    {
        var weaponLootModel = _context.WeaponLoots.FirstOrDefault(
            x => x.RoomId == weaponLoot.RoomId && x.WeaponId == weaponLoot.WeaponId);
        if (weaponLootModel is null)
            return null;
        _context.WeaponLoots.Remove(weaponLootModel);
        await _context.SaveChangesAsync();
        return weaponLootModel;
    }

    public async Task<WeaponLoot?> RemoveOneAsync(WeaponLootRequestDto update)
    {
        var weaponLoot = await _context.WeaponLoots.FirstOrDefaultAsync(
            x => x.WeaponId == update.WeaponId && x.RoomId == update.RoomId);
        if (weaponLoot is null)
            return null;
        
        weaponLoot.Quantity -= 1;
        await _context.SaveChangesAsync();
        return weaponLoot;
    }
}