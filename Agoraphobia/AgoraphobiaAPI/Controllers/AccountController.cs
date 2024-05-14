using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Controllers
{
    [Route("agoraphobia/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IAccountRepository _accountRepository;
        public AccountController(ApplicationDBContext context, IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _accountRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return account is null ? NotFound() : Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequestDto account)
        {
            var accountModel = account.ToAccountFromCreateDto();
            var final = new Account(accountModel.Username, accountModel.Passwd, true);
            await _context.Accounts.AddAsync(final);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = accountModel.Id }, final.ToAccountDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAccountRequestDto account)
        {
            var accountModel = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (accountModel is null)
                return NotFound();

            accountModel.Username = account.Username;
            accountModel.Password.ChangePassword(account.OldPassword, account.NewPassword);
            
            await _context.SaveChangesAsync();
            return Ok(accountModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var accountModel = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (accountModel is null)
                return NotFound();
            
            _context.Accounts.Remove(accountModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
