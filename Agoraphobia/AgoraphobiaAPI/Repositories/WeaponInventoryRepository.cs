using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class WeaponInventoryRepository : IWeaponInventoryRepository
{
    private readonly ApplicationDBContext _context;

    public WeaponInventoryRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<WeaponInventory>> GetWeaponInventoriesAsync(int playerId)
    {
        return await _context.WeaponInventories.Where(x => x.PlayerId == playerId).ToListAsync();
    }

    public async Task<WeaponInventory> CreateAsync(WeaponInventory weaponInventory)
    {
        await _context.WeaponInventories.AddAsync(weaponInventory);
        await _context.SaveChangesAsync();
        return weaponInventory;
    }

    public async Task<WeaponInventory?> AddOneAsync(WeaponInventoryRequestDto update)
    {
        var weaponInventory = await _context.WeaponInventories.FirstOrDefaultAsync(
            x => x.WeaponId == update.WeaponId && x.PlayerId == update.PlayerId);
        if (weaponInventory is null)
            return null;
        
        weaponInventory.Quantity += 1;
        await _context.SaveChangesAsync();
        return weaponInventory;
    }

    public async Task<WeaponInventory?> DeleteAsync(WeaponInventory weaponInventory)
    {
        var weaponInventoryModel = _context.WeaponInventories.FirstOrDefault(
            x => x.PlayerId == weaponInventory.PlayerId && x.WeaponId == weaponInventory.WeaponId);
        if (weaponInventoryModel is null)
            return null;
        _context.WeaponInventories.Remove(weaponInventoryModel);
        await _context.SaveChangesAsync();
        return weaponInventoryModel;
    }

    public async Task<WeaponInventory?> RemoveOneAsync(WeaponInventoryRequestDto update)
    {
        var weaponInventory = await _context.WeaponInventories.FirstOrDefaultAsync(
            x => x.WeaponId == update.WeaponId && x.PlayerId == update.PlayerId);
        if (weaponInventory is null)
            return null;
        
        weaponInventory.Quantity -= 1;
        await _context.SaveChangesAsync();
        return weaponInventory;
    }
}