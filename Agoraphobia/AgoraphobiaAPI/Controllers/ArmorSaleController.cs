using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
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
    }
}
