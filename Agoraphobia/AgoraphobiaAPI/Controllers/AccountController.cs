using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.Exceptions.Account;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [Route("agoraphobia/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
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
            var account = await _accountRepository.GetByIdAsync(id);
            return account is null ? NotFound() : Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequestDto account)
        {
            var accounts = await _accountRepository.GetAllAsync();
            if (accounts.Exists(x => x.Username == account.Username))
                throw new NonUniqueUsernameException();
            var accountModel = account.ToAccountFromCreateDto();
            await _accountRepository.CreateAsync(accountModel);
            return CreatedAtAction(nameof(GetById), new { id = accountModel.Id }, accountModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAccountRequestDto account)
        {
            var accounts = await _accountRepository.GetAllAsync();
            if (accounts.Exists(x => x.Username == account.Username && x.Id != id))
                throw new NonUniqueUsernameException();
            var accountModel = await _accountRepository.UpdateAsync(id, account);
            if (accountModel is null)
                return NotFound();
            return Ok(accountModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var accountModel = await _accountRepository.DeleteAsync(id);
            if (accountModel is null)
                return NotFound();
            return NoContent();
        }
    }
}
