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
        public async Task<IActionResult> GetArmorInventory([FromRoute] int merchantId)
        {
            var merchant = await _merchantRepository.GetByIdAsync(merchantId);
            if (merchant is null)
                return NotFound();
            var armorInventories = await _armorSaleRepository.GetArmorSalesAsync(merchantId);
            return Ok(armorInventories.Select(x => x.ToArmorSaleDto()));
        }

        [HttpPost]
        public async Task<IActionResult> AddToArmorSale([FromBody] ArmorSaleRequestDto armorSaleRequestDto)
        {
            var merchant = await _merchantRepository.GetByIdAsync(armorSaleRequestDto.MerchantId);
            var armor = await _armorRepository.GetByIdAsync(armorSaleRequestDto.ArmorId);
            if (merchant is null)
                return BadRequest("Merchant not found");
            if (armor is null)
                return BadRequest("Armor not found");

            var armorSales = await _armorSaleRepository.GetArmorSalesAsync(armorSaleRequestDto.MerchantId);
            if (armorSales.Exists(x => x.ArmorId == armor.Id))
            {
                var updated = await _armorSaleRepository.AddOneAsync(armorSaleRequestDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToUpdateArmorSaleRequestDto());
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
            return Created("agoraphobia/armorSales", armorSale.ToUpdateArmorSaleRequestDto());
        }
    }
}
