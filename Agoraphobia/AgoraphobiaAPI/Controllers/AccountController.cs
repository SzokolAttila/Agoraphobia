using System.Security.AccessControl;
using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Account;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [Route("agoraphobia/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public AccountController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Accounts.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var account = _context.Accounts.Find(id);
            return account is null ? NotFound() : Ok(account);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAccountRequestDto account)
        {
            var accountModel = account.ToAccountFromCreateDto();
            var final = new Account(accountModel.Username, accountModel.Passwd, true);
            _context.Accounts.Add(final);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = accountModel.Id }, final.ToAccountDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateAccountRequestDto account)
        {
            var accountModel = _context.Accounts.FirstOrDefault(x => x.Id == id);
            if (accountModel is null)
                return NotFound();

            accountModel.Username = account.Username;
            accountModel.Password.ChangePassword(account.OldPassword, account.NewPassword);
            
            _context.SaveChanges();
            return Ok(accountModel);
        }
    }
}
