using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
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
    }
}
