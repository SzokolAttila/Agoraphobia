using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task<Account> CreateAsync(Account accountModel);
        Task<Account?> UpdateAsync(int id, UpdateAccountRequestDto accountDto);
        Task<Account?> DeleteAsync(int id);
    }
}