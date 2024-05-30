using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class RoomMerchantWeaponSaleStatusRepository : IRoomMerchantWeaponSaleStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomMerchantWeaponSaleStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomMerchantWeaponSaleStatus>> GetWeaponSalesAsync(int playerId)
        {
            return await _context.RoomMerchantWeaponSaleStatus
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
        public async Task<RoomMerchantWeaponSaleStatus> CreateAsync(RoomMerchantWeaponSaleStatus status)
        {
            await _context.RoomMerchantWeaponSaleStatus.AddAsync(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<RoomMerchantWeaponSaleStatus?> AddOneAsync(WeaponSaleStatusRequestDto update)
        {
            var status = await _context.RoomMerchantWeaponSaleStatus.FirstOrDefaultAsync(
                x => x.PlayerId == update.PlayerId &&
                     x.RoomId == update.RoomId &&
                     x.WeaponId == update.WeaponId &&
                     x.MerchantId == update.MerchantId);
            if (status is null)
                return null;

            status.Quantity += 1;
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
