using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaAPI.Dtos.RoomEnemyStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class RoomEnemyStatusRepository : IRoomEnemyStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomEnemyStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomEnemyStatus>> GetRoomStatusesAsync(int playerId)
        {
            return await _context.RoomEnemyStatus
                .Where(x => x.PlayerId == playerId)
                .Include(x => x.Room)
                .ThenInclude(x => x.Enemy)
                .ThenInclude(x => x.ArmorDroprates)
                .ThenInclude(x => x.Armor)
                .Include(x => x.Room)
                .ThenInclude(x => x.Enemy)
                .ThenInclude(x => x.ConsumableDroprates)
                .ThenInclude(x => x.Consumable)
                .Include(x => x.Room)
                .ThenInclude(x => x.Enemy)
                .ThenInclude(x => x.WeaponDroprates)
                .ThenInclude(x => x.Weapon)
                .Include(x => x.Player)
                .ThenInclude(x => x.ArmorInventories)
                .ThenInclude(x => x.Armor)
                .Include(x => x.Player)
                .ThenInclude(x => x.WeaponInventories)
                .ThenInclude(x => x.Weapon)
                .Include(x => x.Player)
                .ThenInclude(x => x.ConsumableInventories)
                .ThenInclude(x => x.Consumable)
                .Include(x => x.Room)
                .ThenInclude(x => x.Armors)
                .ThenInclude(x => x.Armor)
                .Include(x => x.Room)
                .ThenInclude(x => x.Consumables)
                .ThenInclude(x => x.Consumable)
                .Include(x => x.Room)
                .ThenInclude(x => x.Weapons)
                .ThenInclude(x => x.Weapon)
                .Include(x => x.Room)
                .ThenInclude(x => x.Merchant)
                .ThenInclude(x => x.ArmorSales)
                .ThenInclude(x => x.Armor)
                .Include(x => x.Room)
                .ThenInclude(x => x.Merchant)
                .ThenInclude(x => x.ConsumableSales)
                .ThenInclude(x => x.Consumable)
                .Include(x => x.Room)
                .ThenInclude(x => x.Merchant)
                .ThenInclude(x => x.WeaponSales)
                .ThenInclude(x => x.Weapon)
                .ToListAsync();
        }

        public async Task<RoomEnemyStatus> CreateRoomStatusAsync(RoomEnemyStatus roomStatus)
        {
            await _context.RoomEnemyStatus.AddAsync(roomStatus);
            await _context.SaveChangesAsync();
            return roomStatus;
        }

        public async Task<RoomEnemyStatus?> UpdateRoomStatusAsync(CreateRoomEnemyStatusDto roomStatus)
        {
            var roomStatusModel = await _context.RoomEnemyStatus.FirstOrDefaultAsync(x => x.PlayerId == roomStatus.PlayerId && x.RoomId == roomStatus.RoomId);
            if (roomStatusModel is null)
                return null;

            roomStatusModel.EnemyHp = roomStatus.EnemyHp;

            await _context.SaveChangesAsync();
            return roomStatusModel;
        }
    }
}
