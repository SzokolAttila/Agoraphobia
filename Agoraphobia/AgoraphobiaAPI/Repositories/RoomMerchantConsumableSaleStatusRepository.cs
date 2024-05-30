using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.RoomMerchantConsumableSaleStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class RoomMerchantConsumableSaleStatusRepository : IRoomMerchantConsumableSaleStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomMerchantConsumableSaleStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomMerchantConsumableSaleStatus>> GetConsumableSalesAsync(int playerId)
        {
            return await _context.RoomMerchantConsumableSaleStatus
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
        public async Task<RoomMerchantConsumableSaleStatus> CreateAsync(RoomMerchantConsumableSaleStatus status)
        {
            await _context.RoomMerchantConsumableSaleStatus.AddAsync(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<RoomMerchantConsumableSaleStatus?> AddOneAsync(ConsumableSaleStatusRequestDto update)
        {
            var status = await _context.RoomMerchantConsumableSaleStatus.FirstOrDefaultAsync(
                x => x.PlayerId == update.PlayerId &&
                     x.RoomId == update.RoomId &&
                     x.ConsumableId == update.ConsumableId &&
                     x.MerchantId == update.MerchantId);
            if (status is null)
                return null;

            status.Quantity += 1;
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
