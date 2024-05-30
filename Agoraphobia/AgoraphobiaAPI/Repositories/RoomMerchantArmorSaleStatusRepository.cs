using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
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

    }
}
