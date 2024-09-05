using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Weapons;
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
            return await _context.WeaponSales
                .Include(x => x.Weapon)
                .Where(x => x.MerchantId == merchantId).ToListAsync();
        }
        public async Task<WeaponSale> CreateAsync(WeaponSale weaponSale)
        {
            await _context.WeaponSales.AddAsync(weaponSale);
            await _context.SaveChangesAsync();
            return weaponSale;
        }

        public async Task<WeaponSale?> AddOneAsync(int id)
        {
            var weaponSale = await _context.WeaponSales.FirstOrDefaultAsync(x => x.Id == id);
            if (weaponSale is null)
                return null;

            weaponSale.Quantity += 1;
            await _context.SaveChangesAsync();
            return weaponSale;
        }
        public async Task<WeaponSale?> DeleteAsync(int id)
        {
            var weaponSaleModel = _context.WeaponSales.FirstOrDefault(x => x.Id == id);
            if (weaponSaleModel is null)
                return null;
            _context.WeaponSales.Remove(weaponSaleModel);
            await _context.SaveChangesAsync();
            return weaponSaleModel;
        }

        public async Task<WeaponSale?> GetByIdAsync(int id)
        {
            return await _context.WeaponSales
                .Include(x => x.Weapon)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<WeaponSale?> RemoveOneAsync(int id)
        {
            var weaponSale = await _context.WeaponSales.FirstOrDefaultAsync(x => x.Id == id);
            if (weaponSale is null)
                return null;

            weaponSale.Quantity -= 1;
            await _context.SaveChangesAsync();
            return weaponSale;
        }
    }
}
