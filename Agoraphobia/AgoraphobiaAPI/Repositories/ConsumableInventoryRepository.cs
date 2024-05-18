using AgoraphobiaAPI.Data;
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
}