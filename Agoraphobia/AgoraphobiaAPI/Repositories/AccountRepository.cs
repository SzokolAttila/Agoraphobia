using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDBContext _context;
    public AccountRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Account>> GetAllAsync()
    {
        return await _context.Accounts
            .Include(x => x.Players)
            .ThenInclude(x => x.WeaponInventories)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.Players)
            .ThenInclude(x => x.ArmorInventories)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Players)
            .ThenInclude(x => x.ConsumableInventories)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Effects)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Enemy)
            .ThenInclude(x => x.ArmorDroprates)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Enemy)
            .ThenInclude(x => x.ConsumableDroprates)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Enemy)
            .ThenInclude(x => x.WeaponDroprates)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Armors)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Consumables)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Weapons)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Merchant)
            .ThenInclude(x => x.ArmorSales)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Merchant)
            .ThenInclude(x => x.ConsumableSales)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Merchant)
            .ThenInclude(x => x.WeaponSales)
            .ThenInclude(x => x.Weapon)
            .ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _context.Accounts
            .Include(x => x.Players)
            .ThenInclude(x => x.WeaponInventories)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.Players)
            .ThenInclude(x => x.ArmorInventories)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Players)
            .ThenInclude(x => x.ConsumableInventories)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Effects)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Enemy)
            .ThenInclude(x => x.ArmorDroprates)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Enemy)
            .ThenInclude(x => x.ConsumableDroprates)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Enemy)
            .ThenInclude(x => x.WeaponDroprates)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Armors)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Consumables)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Weapons)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Merchant)
            .ThenInclude(x => x.ArmorSales)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Merchant)
            .ThenInclude(x => x.ConsumableSales)
            .ThenInclude(x => x.Consumable)
            .Include(x => x.Players)
            .ThenInclude(x => x.Room)
            .ThenInclude(x => x.Merchant)
            .ThenInclude(x => x.WeaponSales)
            .ThenInclude(x => x.Weapon)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Account> CreateAsync(Account accountModel)
    {
        await _context.Accounts.AddAsync(accountModel);
        await _context.SaveChangesAsync();
        return accountModel;
    }

    public async Task<Account?> UpdateAsync(int id, UpdateAccountRequestDto accountDto)
    {
        var accountModel = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        if (accountModel is null)
            return null;
        
        accountModel.Username = accountDto.Username;
        accountModel.Password.ChangePassword(accountDto.OldPassword, accountDto.NewPassword);
        await _context.SaveChangesAsync();
        return accountModel;
    }

    public async Task<Account?> DeleteAsync(int id)
    {
        var accountModel = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        if (accountModel is null)
            return null;
        _context.Accounts.Remove(accountModel);
        await _context.SaveChangesAsync();
        return accountModel;
    }
}