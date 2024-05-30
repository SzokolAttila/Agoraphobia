using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaAPI.Dtos.RoomMerchantArmorSaleStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class RoomMerchantArmorSaleStatusRepository : IRoomMerchantArmorSaleStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public RoomMerchantArmorSaleStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<RoomMerchantArmorSaleStatus>> GetArmorSalesAsync(int playerId)
        {
            return await _context.RoomMerchantArmorSaleStatus
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
        public async Task<RoomMerchantArmorSaleStatus> CreateAsync(RoomMerchantArmorSaleStatus status)
        {
            await _context.RoomMerchantArmorSaleStatus.AddAsync(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<RoomMerchantArmorSaleStatus?> AddOneAsync(ArmorSaleStatusRequestDto update)
        {
            var status = await _context.RoomMerchantArmorSaleStatus.FirstOrDefaultAsync(
                x => x.PlayerId == update.PlayerId &&
                     x.RoomId == update.RoomId &&
                     x.ArmorId == update.ArmorId &&
                     x.MerchantId == update.MerchantId);
            if (status is null)
                return null;

            status.Quantity += 1;
            await _context.SaveChangesAsync();
            return status;
        }
        public async Task<RoomMerchantArmorSaleStatus?> DeleteAsync(RoomMerchantArmorSaleStatus status)
        {
            var statusModel = _context.RoomMerchantArmorSaleStatus.FirstOrDefault(
                x => x.ArmorId == status.ArmorId 
                     && x.PlayerId == status.PlayerId 
                     && x.RoomId == status.RoomId 
                     && x.MerchantId == status.MerchantId);
            if (statusModel is null)
                return null;
            _context.RoomMerchantArmorSaleStatus.Remove(status);
            await _context.SaveChangesAsync();
            return statusModel;
        }

        public async Task<RoomMerchantArmorSaleStatus?> RemoveOneAsync(ArmorSaleStatusRequestDto update)
        {
            var status = await _context.RoomMerchantArmorSaleStatus.FirstOrDefaultAsync(
                x => x.ArmorId == update.ArmorId 
                     && x.PlayerId == update.PlayerId 
                     && x.RoomId == update.RoomId 
                     && x.MerchantId == update.MerchantId);
            if (status is null)
                return null;

            status.Quantity -= 1;
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
