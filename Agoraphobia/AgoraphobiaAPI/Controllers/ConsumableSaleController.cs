using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/consumableSales")]
    public class ConsumableSaleController : ControllerBase
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IConsumableRepository _consumableRepository;
        private readonly IConsumableSaleRepository _consumableSaleRepository;
        public ConsumableSaleController(
            IMerchantRepository merchantRepository,
            IConsumableRepository consumableRepository,
            IConsumableSaleRepository consumableSaleRepository)
        {
            _merchantRepository = merchantRepository;
            _consumableRepository = consumableRepository;
            _consumableSaleRepository = consumableSaleRepository;
        }
        [HttpGet("{merchantId}")]
        public async Task<IActionResult> GetConsumableSales([FromRoute] int merchantId)
        {
            var merchant = await _merchantRepository.GetByIdAsync(merchantId);
            if (merchant is null)
                return NotFound();
            var consumableInventories = await _consumableSaleRepository.GetConsumableSalesAsync(merchantId);
            return Ok(consumableInventories.Select(x => x.ToConsumableSaleDto()));
        }
    }
}
