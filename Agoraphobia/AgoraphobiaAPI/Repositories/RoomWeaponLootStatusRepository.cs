using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.RoomWeaponLootStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class RoomWeaponLootStatusRepository : IRoomWeaponLootStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomWeaponLootStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomWeaponLootStatus>> GetRoomWeaponLootStatusesAsync(int playerId)
        {
            return await _context.RoomWeaponLootStatus
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
                .Include(x => x.Weapon)
                .ToListAsync();
        }
        public async Task<RoomWeaponLootStatus> CreateAsync(RoomWeaponLootStatus status)
        {
            await _context.RoomWeaponLootStatus.AddAsync(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<RoomWeaponLootStatus?> GetByIdAsync(int weaponLootStatusId)
        {
            return await _context.RoomWeaponLootStatus
                .Include(x => x.Weapon)
                .FirstOrDefaultAsync(x => x.Id == weaponLootStatusId);
        }

        public async Task<RoomWeaponLootStatus?> AddOneAsync(WeaponLootStatusRequestDto update)
        {
            var status = await _context.RoomWeaponLootStatus.FirstOrDefaultAsync(
                x => x.PlayerId == update.PlayerId &&
                     x.RoomId == update.RoomId &&
                     x.WeaponId == update.WeaponId);
            if (status is null)
                return null;

            status.Quantity += 1;
            await _context.SaveChangesAsync();
            return status;
        }
        public async Task<RoomWeaponLootStatus?> DeleteAsync(RoomWeaponLootStatus status)
        {
            var statusModel = _context.RoomWeaponLootStatus.FirstOrDefault(
                x => x.WeaponId == status.WeaponId && x.PlayerId == status.PlayerId && x.RoomId == status.RoomId);
            if (statusModel is null)
                return null;
            _context.RoomWeaponLootStatus.Remove(status);
            await _context.SaveChangesAsync();
            return statusModel;
        }

        public async Task<RoomWeaponLootStatus?> RemoveOneAsync(WeaponLootStatusRequestDto update)
        {
            var status = await _context.RoomWeaponLootStatus.FirstOrDefaultAsync(
                x => x.WeaponId == update.WeaponId && x.PlayerId == update.PlayerId && x.RoomId == update.RoomId);
            if (status is null)
                return null;

            status.Quantity -= 1;
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
