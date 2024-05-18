using AgoraphobiaAPI.Dtos.Weapon;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [Route("agoraphobia/weapons")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponRepository _weaponRepository;
        public WeaponController(IWeaponRepository weaponRepository)
        {
            _weaponRepository = weaponRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var weapons = await _weaponRepository.GetAllAsync();
            return Ok(weapons.Select(x => x.ToWeaponDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var weapon = await _weaponRepository.GetByIdAsync(id);
            return weapon is null ? NotFound() : Ok(weapon.ToWeaponDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWeaponRequestDto weapon)
        {
            var weaponModel = weapon.ToWeaponFromCreateDto();
            await _weaponRepository.CreateAsync(weaponModel);
            return CreatedAtAction(nameof(GetById), new { id = weaponModel.Id }, weaponModel.ToWeaponDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateWeaponRequestDto weapon)
        {
            var weaponModel = await _weaponRepository.UpdateAsync(id, weapon);
            if (weaponModel is null)
                return NotFound();
            return Ok(weaponModel.ToWeaponDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var weaponModel = await _weaponRepository.DeleteAsync(id);
            if (weaponModel is null)
                return NotFound();
            return NoContent();
        }
    }
}
