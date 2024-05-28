using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ArmorSale;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Armors;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class ArmorSaleRepository : IArmorSaleRepository
    {
        private readonly ApplicationDBContext _context;
        public ArmorSaleRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<ArmorSale>> GetArmorSalesAsync(int merchantId)
        {
            return await _context.ArmorSales.Where(x => x.MerchantId == merchantId).ToListAsync();
        }
        public async Task<ArmorSale> CreateAsync(ArmorSale armorSale)
        {
            await _context.ArmorSales.AddAsync(armorSale);
            await _context.SaveChangesAsync();
            return armorSale;
        }

        public async Task<ArmorSale?> AddOneAsync(ArmorSaleRequestDto update)
        {
            var armorSale = await _context.ArmorSales.FirstOrDefaultAsync(
                x => x.ArmorId == update.ArmorId && x.MerchantId == update.MerchantId);
            if (armorSale is null)
                return null;

            armorSale.Quantity += 1;
            await _context.SaveChangesAsync();
            return armorSale;
        }
    }
}
