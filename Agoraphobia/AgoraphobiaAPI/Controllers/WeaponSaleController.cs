using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/weaponSales")]
    public class WeaponSaleController : ControllerBase
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IWeaponSaleRepository _weaponSaleRepository;
        public WeaponSaleController(
            IMerchantRepository merchantRepository,
            IWeaponRepository weaponRepository,
            IWeaponSaleRepository weaponSaleRepository)
        {
            _merchantRepository = merchantRepository;
            _weaponRepository = weaponRepository;
            _weaponSaleRepository = weaponSaleRepository;
        }
        [HttpGet("{merchantId}")]
        public async Task<IActionResult> GetWeaponSales([FromRoute] int merchantId)
        {
            var merchant = await _merchantRepository.GetByIdAsync(merchantId);
            if (merchant is null)
                return NotFound();
            var weaponSales = await _weaponSaleRepository.GetWeaponSalesAsync(merchantId);
            return Ok(weaponSales.Select(x => x.ToWeaponSaleDto()));
        }
    }
}
