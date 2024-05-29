using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories
{
    public class ConsumableSaleRepository : IConsumableSaleRepository
    {
        private readonly ApplicationDBContext _context;
        public ConsumableSaleRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<ConsumableSale>> GetConsumableSalesAsync(int merchantId)
        {
            return await _context.ConsumableSales.Where(x => x.MerchantId == merchantId).ToListAsync();
        }
    }
}
