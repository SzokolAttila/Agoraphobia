using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly ApplicationDBContext _context;

        public MerchantRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Merchant>> GetAllAsync()
        {
            return await _context.Merchants
                .Include(x => x.ConsumableSales)
                .ThenInclude(x => x.Consumable)
                .Include(x => x.ArmorSales)
                .ThenInclude(x => x.Armor)
                .Include(x => x.WeaponSales)
                .ThenInclude(x => x.Weapon)
                .ToListAsync();
        }

        public async Task<Merchant?> GetByIdAsync(int id)
        {
            return await _context.Merchants
                .Include(x => x.WeaponSales)
                .ThenInclude(x => x.Weapon)
                .Include(x => x.ArmorSales)
                .ThenInclude(x => x.Armor)
                .Include(x => x.ConsumableSales)
                .ThenInclude(x => x.Consumable)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
