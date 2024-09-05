using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.JoinTables.Consumables;
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
        [HttpPost]
        public async Task<IActionResult> AddToConsumableSales([FromBody] ConsumableSaleRequestDto consumableSaleRequestDto)
        {
            var merchant = await _merchantRepository.GetByIdAsync(consumableSaleRequestDto.MerchantId);
            var consumable = await _consumableRepository.GetByIdAsync(consumableSaleRequestDto.ConsumableId);
            if (merchant is null)
                return BadRequest("Merchant not found");
            if (consumable is null)
                return BadRequest("Consumable not found");

            var consumableSales = await _consumableSaleRepository
                .GetConsumableSalesAsync(consumableSaleRequestDto.MerchantId);
            var createdSale = consumableSales.Find(x => x.ConsumableId == consumable.Id);
            if (createdSale != null)
            {
                var updated = await _consumableSaleRepository.AddOneAsync(createdSale.Id);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToConsumableSaleDto());
            }
            var consumableSale = new ConsumableSale
            {
                MerchantId = consumableSaleRequestDto.MerchantId,
                ConsumableId = consumableSaleRequestDto.ConsumableId,
                Quantity = 1,
                Consumable = consumable
            };
            await _consumableSaleRepository.CreateAsync(consumableSale);
            return Created("agoraphobia/consumableSales", consumableSale.ToConsumableSaleDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromConsumableSales([FromRoute] int id)
        {
            var consumableSale = await _consumableSaleRepository.GetByIdAsync(id);
            if (consumableSale is null)
                return NotFound();

            if (consumableSale.Quantity > 1)
            {
                var updated = await _consumableSaleRepository.RemoveOneAsync(id);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToConsumableSaleDto());
            }

            await _consumableSaleRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
