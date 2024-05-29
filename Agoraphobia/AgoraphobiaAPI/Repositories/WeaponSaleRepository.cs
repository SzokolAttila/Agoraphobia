using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Weapons;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class WeaponSaleRepository : IWeaponSaleRepository
    {
        private readonly ApplicationDBContext _context;
        public WeaponSaleRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<WeaponSale>> GetWeaponSalesAsync(int merchantId)
        {
            return await _context.WeaponSales.Where(x => x.MerchantId == merchantId).ToListAsync();
        }
        public async Task<WeaponSale> CreateAsync(WeaponSale weaponSale)
        {
            await _context.WeaponSales.AddAsync(weaponSale);
            await _context.SaveChangesAsync();
            return weaponSale;
        }

        public async Task<WeaponSale?> AddOneAsync(WeaponSaleRequestDto update)
        {
            var weaponSale = await _context.WeaponSales.FirstOrDefaultAsync(
                x => x.WeaponId == update.WeaponId && x.MerchantId == update.MerchantId);
            if (weaponSale is null)
                return null;

            weaponSale.Quantity += 1;
            await _context.SaveChangesAsync();
            return weaponSale;
        }
    }
}
