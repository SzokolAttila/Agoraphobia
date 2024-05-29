using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary.JoinTables.Weapons;
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
        [HttpPost]
        public async Task<IActionResult> AddToWeaponSales([FromBody] WeaponSaleRequestDto weaponSaleRequestDto)
        {
            var merchant = await _merchantRepository.GetByIdAsync(weaponSaleRequestDto.MerchantId);
            var weapon = await _weaponRepository.GetByIdAsync(weaponSaleRequestDto.WeaponId);
            if (merchant is null)
                return BadRequest("Merchant not found");
            if (weapon is null)
                return BadRequest("Weapon not found");

            var weaponSales = await _weaponSaleRepository.GetWeaponSalesAsync(weaponSaleRequestDto.MerchantId);
            if (weaponSales.Exists(x => x.WeaponId == weapon.Id))
            {
                var updated = await _weaponSaleRepository.AddOneAsync(weaponSaleRequestDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToUpdateWeaponSaleRequestDto());
            }
            var weaponSale = new WeaponSale
            {
                MerchantId = weaponSaleRequestDto.MerchantId,
                WeaponId = weaponSaleRequestDto.WeaponId,
                Quantity = 1,
                Merchant = merchant,
                Weapon = weapon
            };
            await _weaponSaleRepository.CreateAsync(weaponSale);
            return Created("agoraphobia/weaponSales", weaponSale.ToUpdateWeaponSaleRequestDto());
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveFromWeaponSales([FromBody] WeaponSaleRequestDto weaponSaleRequestDto)
        {
            var merchant = await _merchantRepository.GetByIdAsync(weaponSaleRequestDto.MerchantId);
            var weapon = await _weaponRepository.GetByIdAsync(weaponSaleRequestDto.WeaponId);
            if (merchant is null)
                return BadRequest("Merchant not found");
            if (weapon is null)
                return BadRequest("Weapon not found");

            var weaponInventories = await _weaponSaleRepository.GetWeaponSalesAsync(merchant.Id);
            var weaponSale = weaponInventories.FirstOrDefault(x => x.WeaponId == weapon.Id);
            if (weaponSale is null)
                return NotFound();

            if (weaponSale.Quantity > 1)
            {
                var updated = await _weaponSaleRepository.RemoveOneAsync(weaponSaleRequestDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToUpdateWeaponSaleRequestDto());
            }

            await _weaponSaleRepository.DeleteAsync(weaponSale);
            return NoContent();
        }
    }
}
