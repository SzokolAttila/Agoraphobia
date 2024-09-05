using AgoraphobiaAPI.Dtos.ArmorSale;
using AgoraphobiaAPI.Dtos.ArmorSale;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary.JoinTables.Armors;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/armorSales")]
    public class ArmorSaleController : ControllerBase
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IArmorRepository _armorRepository;
        private readonly IArmorSaleRepository _armorSaleRepository;
        public ArmorSaleController(
            IMerchantRepository merchantRepository,
            IArmorRepository armorRepository,
            IArmorSaleRepository armorSaleRepository)
        {
            _merchantRepository = merchantRepository;
            _armorRepository = armorRepository;
            _armorSaleRepository = armorSaleRepository;
        }
        [HttpGet("{merchantId}")]
        public async Task<IActionResult> GetArmorSales([FromRoute] int merchantId)
        {
            var merchant = await _merchantRepository.GetByIdAsync(merchantId);
            if (merchant is null)
                return NotFound();
            var armorInventories = await _armorSaleRepository.GetArmorSalesAsync(merchantId);
            return Ok(armorInventories.Select(x => x.ToArmorSaleDto()));
        }

        [HttpPost]
        public async Task<IActionResult> AddToArmorSales([FromBody] ArmorSaleRequestDto armorSaleRequestDto)
        {
            var merchant = await _merchantRepository.GetByIdAsync(armorSaleRequestDto.MerchantId);
            var armor = await _armorRepository.GetByIdAsync(armorSaleRequestDto.ArmorId);
            if (merchant is null)
                return BadRequest("Merchant not found");
            if (armor is null)
                return BadRequest("Armor not found");

            var armorSales = await _armorSaleRepository.GetArmorSalesAsync(armorSaleRequestDto.MerchantId);
            var createdSale = armorSales.Find(x => x.ArmorId == armor.Id);
            if (createdSale != null)
            {
                var updated = await _armorSaleRepository.AddOneAsync(createdSale.Id);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToArmorSaleDto());
            }
            var armorSale = new ArmorSale
            {
                MerchantId = armorSaleRequestDto.MerchantId,
                ArmorId = armorSaleRequestDto.ArmorId,
                Quantity = 1,
                Merchant = merchant,
                Armor = armor
            };
            await _armorSaleRepository.CreateAsync(armorSale);
            return Created("agoraphobia/armorSales", armorSale.ToArmorSaleDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromArmorSales([FromRoute] int id)
        {
            var armorSale = await _armorSaleRepository.GetByIdAsync(id);
            if (armorSale is null)
                return NotFound();

            if (armorSale.Quantity > 1)
            {
                var updated = await _armorSaleRepository.RemoveOneAsync(id);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToArmorSaleDto());
            }

            await _armorSaleRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
