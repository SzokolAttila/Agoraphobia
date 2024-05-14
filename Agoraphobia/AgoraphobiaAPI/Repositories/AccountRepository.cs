using AgoraphobiaAPI.Data;
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
    public Task<List<Account>> GetAllAsync()
    {
        return _context.Accounts.ToListAsync();
    }
}