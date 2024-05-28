using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
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
    }
}
