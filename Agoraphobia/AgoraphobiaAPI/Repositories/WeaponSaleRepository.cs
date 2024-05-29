using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
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
    }
}
