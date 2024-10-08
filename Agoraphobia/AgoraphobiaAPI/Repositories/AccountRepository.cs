﻿using AgoraphobiaAPI.Data;
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
            .ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _context.Accounts
            .Include(x => x.Players)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Account> CreateAsync(Account accountModel)
    {
        await _context.Accounts.AddAsync(accountModel);
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