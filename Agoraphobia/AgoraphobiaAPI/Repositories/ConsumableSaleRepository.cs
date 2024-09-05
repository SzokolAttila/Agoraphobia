using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Consumables;
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
            return await _context.ConsumableSales
                .Include(x => x.Consumable)
                .Where(x => x.MerchantId == merchantId).ToListAsync();
        }
        public async Task<ConsumableSale> CreateAsync(ConsumableSale consumableSale)
        {
            await _context.ConsumableSales.AddAsync(consumableSale);
            await _context.SaveChangesAsync();
            return consumableSale;
        }

        public async Task<ConsumableSale?> AddOneAsync(int id)
        {
            var consumableSale = await _context.ConsumableSales.FirstOrDefaultAsync(x => x.Id == id);
            if (consumableSale is null)
                return null;

            consumableSale.Quantity += 1;
            await _context.SaveChangesAsync();
            return consumableSale;
        }
        public async Task<ConsumableSale?> DeleteAsync(int id)
        {
            var consumableSaleModel = _context.ConsumableSales.FirstOrDefault(x => x.Id == id);
            if (consumableSaleModel is null)
                return null;
            _context.ConsumableSales.Remove(consumableSaleModel);
            await _context.SaveChangesAsync();
            return consumableSaleModel;
        }

        public async Task<ConsumableSale?> GetByIdAsync(int id)
        {
            return await _context.ConsumableSales
                .Include(x => x.Consumable)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ConsumableSale?> RemoveOneAsync(int id)
        {
            var consumableSale = await _context.ConsumableSales.FirstOrDefaultAsync(x => x.Id == id);
            if (consumableSale is null)
                return null;

            consumableSale.Quantity -= 1;
            await _context.SaveChangesAsync();
            return consumableSale;
        }
    }
}
