using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/merchants")]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantController(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var merchants = await _merchantRepository.GetAllAsync();
            return Ok(merchants.Select(x => x.ToMerchantDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var merchant = await _merchantRepository.GetByIdAsync(id);
            if (merchant is null)
                return NotFound();

            return Ok(merchant.ToMerchantDto());
        }
    }
}
