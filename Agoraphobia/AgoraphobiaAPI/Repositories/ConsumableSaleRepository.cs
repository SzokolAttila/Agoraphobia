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
            return await _context.ConsumableSales.Where(x => x.MerchantId == merchantId).ToListAsync();
        }
        public async Task<ConsumableSale> CreateAsync(ConsumableSale consumableSale)
        {
            await _context.ConsumableSales.AddAsync(consumableSale);
            await _context.SaveChangesAsync();
            return consumableSale;
        }

        public async Task<ConsumableSale?> AddOneAsync(ConsumableSaleRequestDto update)
        {
            var consumableSale = await _context.ConsumableSales.FirstOrDefaultAsync(
                x => x.ConsumableId == update.ConsumableId && x.MerchantId == update.MerchantId);
            if (consumableSale is null)
                return null;

            consumableSale.Quantity += 1;
            await _context.SaveChangesAsync();
            return consumableSale;
        }
        public async Task<ConsumableSale?> DeleteAsync(ConsumableSale consumableSale)
        {
            var consumableSaleModel = _context.ConsumableSales.FirstOrDefault(
                x => x.MerchantId == consumableSale.MerchantId && x.ConsumableId == consumableSale.ConsumableId);
            if (consumableSaleModel is null)
                return null;
            _context.ConsumableSales.Remove(consumableSale);
            await _context.SaveChangesAsync();
            return consumableSaleModel;
        }
        public async Task<ConsumableSale?> RemoveOneAsync(ConsumableSaleRequestDto update)
        {
            var consumableSale = await _context.ConsumableSales.FirstOrDefaultAsync(
                x => x.ConsumableId == update.ConsumableId && x.MerchantId == update.MerchantId);
            if (consumableSale is null)
                return null;

            consumableSale.Quantity -= 1;
            await _context.SaveChangesAsync();
            return consumableSale;
        }
    }
}
