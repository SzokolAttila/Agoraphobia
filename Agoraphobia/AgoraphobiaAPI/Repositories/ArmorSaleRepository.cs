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
            return await _context.ArmorSales
                .Include(x => x.Armor)
                .Where(x => x.MerchantId == merchantId).ToListAsync();
        }
        public async Task<ArmorSale> CreateAsync(ArmorSale armorSale)
        {
            await _context.ArmorSales.AddAsync(armorSale);
            await _context.SaveChangesAsync();
            return armorSale;
        }

        public async Task<ArmorSale?> AddOneAsync(int id)
        {
            var armorSale = await _context.ArmorSales.FirstOrDefaultAsync(x => x.Id == id);
            if (armorSale is null)
                return null;

            armorSale.Quantity += 1;
            await _context.SaveChangesAsync();
            return armorSale;
        }
        public async Task<ArmorSale?> DeleteAsync(int id)
        {
            var armorSaleModel = _context.ArmorSales.FirstOrDefault(x => x.Id == id);
            if (armorSaleModel is null)
                return null;
            _context.ArmorSales.Remove(armorSaleModel);
            await _context.SaveChangesAsync();
            return armorSaleModel;
        }

        public async Task<ArmorSale?> GetByIdAsync(int id)
        {
            return await _context.ArmorSales
                .Include(x => x.Armor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ArmorSale?> RemoveOneAsync(int id)
        {
            var armorSale = await _context.ArmorSales.FirstOrDefaultAsync(x => x.Id == id);
            if (armorSale is null)
                return null;

            armorSale.Quantity -= 1;
            await _context.SaveChangesAsync();
            return armorSale;
        }
    }
}
