using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
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

}