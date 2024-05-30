using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class RoomArmorLootStatusRepository : IRoomArmorLootStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomArmorLootStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomArmorLootStatus>> GetRoomArmorLootStatusesAsync(int playerId)
        {
            return await _context.RoomArmorLootStatus
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
                .Include(x => x.Armor)
                .ToListAsync();
        }

        public async Task<RoomArmorLootStatus> CreateAsync(RoomArmorLootStatus status)
        {
            await _context.RoomArmorLootStatus.AddAsync(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<RoomArmorLootStatus?> AddOneAsync(ArmorLootStatusRequestDto update)
        {
            var status = await _context.RoomArmorLootStatus.FirstOrDefaultAsync(
                x => x.PlayerId == update.PlayerId && 
                     x.RoomId == update.RoomId && 
                     x.ArmorId == update.ArmorId);
            if (status is null)
                return null;

            status.Quantity += 1;
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<RoomArmorLootStatus?> DeleteAsync(RoomArmorLootStatus status)
        {
            var statusModel = _context.RoomArmorLootStatus.FirstOrDefault(
                x => x.ArmorId == status.ArmorId && x.PlayerId == status.PlayerId && x.RoomId == status.RoomId );
            if (statusModel is null)
                return null;
            _context.RoomArmorLootStatus.Remove(status);
            await _context.SaveChangesAsync();
            return statusModel;
        }

        public async Task<RoomArmorLootStatus?> RemoveOneAsync(ArmorLootStatusRequestDto update)
        {
            var status = await _context.RoomArmorLootStatus.FirstOrDefaultAsync(
                x => x.ArmorId == update.ArmorId && x.PlayerId == update.PlayerId && x.RoomId == update.RoomId);
            if (status is null)
                return null;

            status.Quantity -= 1;
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
