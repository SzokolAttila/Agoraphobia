using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.RoomConsumableLootStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class RoomConsumableLootStatusRepository : IRoomConsumableLootStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomConsumableLootStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomConsumableLootStatus>> GetRoomConsumableLootStatusesAsync(int playerId)
        {
            return await _context.RoomConsumableLootStatus
                .Where(x => x.PlayerId == playerId)
                .Include(x => x.Room)
                .ThenInclude(x => x.Weapons)
                .ThenInclude(x => x.Weapon)
                .Include(x => x.Room)
                .ThenInclude(x => x.Armors)
                .ThenInclude(x => x.Armor)
                .Include(x => x.Room)
                .ThenInclude(x => x.Consumables)
                .ThenInclude(x => x.Consumable)
                .Include(x => x.Room)
                .ThenInclude(x => x.Enemy)
                .ThenInclude(x => x.WeaponDroprates)
                .ThenInclude(x => x.Weapon)
                .Include(x => x.Room)
                .ThenInclude(x => x.Enemy)
                .ThenInclude(x => x.ArmorDroprates)
                .ThenInclude(x => x.Armor)
                .Include(x => x.Room)
                .ThenInclude(x => x.Enemy)
                .ThenInclude(x => x.ConsumableDroprates)
                .ThenInclude(x => x.Consumable)
                .Include(x => x.Room)
                .ThenInclude(x => x.Merchant)
                .ThenInclude(x => x.ArmorSales)
                .ThenInclude(x => x.Armor)
                .Include(x => x.Room)
                .ThenInclude(x => x.Merchant)
                .ThenInclude(x => x.WeaponSales)
                .ThenInclude(x => x.Weapon)
                .Include(x => x.Room)
                .ThenInclude(x => x.Merchant)
                .ThenInclude(x => x.ConsumableSales)
                .ThenInclude(x => x.Consumable)
                .Include(x => x.Consumable)
                .ToListAsync();
        }
         public async Task<RoomConsumableLootStatus> CreateAsync(RoomConsumableLootStatus status)
        {
            await _context.RoomConsumableLootStatus.AddAsync(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<RoomConsumableLootStatus?> AddOneAsync(ConsumableLootStatusRequestDto update)
        {
            var status = await _context.RoomConsumableLootStatus.FirstOrDefaultAsync(
                x => x.PlayerId == update.PlayerId && 
                     x.RoomId == update.RoomId && 
                     x.ConsumableId == update.ConsumableId);
            if (status is null)
                return null;

            status.Quantity += 1;
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
