using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;

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

        public async Task<Merchant> CreateAsync(Merchant merchant)
        {
            await _context.Merchants.AddAsync(merchant);
            await _context.SaveChangesAsync();
            return merchant;
        }

        public async Task<Merchant?> DeleteAsync(int id)
        {
            var merchant = await _context.Merchants.FirstOrDefaultAsync(x => x.Id == id);
            if (merchant is null)
                return null;
            _context.Merchants.Remove(merchant);
            await _context.SaveChangesAsync();
            return merchant;
        }
    }
}
