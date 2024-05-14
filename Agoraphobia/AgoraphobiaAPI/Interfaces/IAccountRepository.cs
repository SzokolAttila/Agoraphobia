using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAsync();
    }
}