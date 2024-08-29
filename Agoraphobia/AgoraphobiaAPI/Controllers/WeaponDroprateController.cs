using AgoraphobiaAPI.Dtos.WeaponDroprate;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[Route("agoraphobia/weaponDroprates")]
[ApiController]
public class WeaponDroprateController : ControllerBase
{
    private readonly IEnemyRepository _enemyRepository;
    private readonly IWeaponRepository _weaponRepository;
    private readonly IWeaponDroprateRepository _weaponDroprateRepository;
    public WeaponDroprateController(
        IEnemyRepository enemyRepository,
        IWeaponRepository weaponRepository,
        IWeaponDroprateRepository weaponDroprateRepository
        )
    {
        _enemyRepository = enemyRepository;
        _weaponRepository = weaponRepository;
        _weaponDroprateRepository = weaponDroprateRepository;
    }

    [HttpGet("{enemyId}")]
    public async Task<IActionResult> GetWeaponDroprate([FromRoute] int enemyId)
    {
        var enemy = await _enemyRepository.GetByIdAsync(enemyId);
        if (enemy is null)
            return NotFound();
        var weaponDroprates = await _weaponDroprateRepository.GetWeaponDropratesAsync(enemyId);
        return Ok(weaponDroprates.Select(x => x.ToWeaponDroprateDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToWeaponDroprate([FromBody] WeaponDroprateRequestDto weaponDroprateRequestDto)
    {
        var enemy = await _enemyRepository.GetByIdAsync(weaponDroprateRequestDto.EnemyId);
        var weapon = await _weaponRepository.GetByIdAsync(weaponDroprateRequestDto.WeaponId);
        if (enemy is null)
            return BadRequest("Enemy not found");
        if (weapon is null)
            return BadRequest("Weapon not found");
        
        var weaponDroprates = await _weaponDroprateRepository.GetWeaponDropratesAsync(weaponDroprateRequestDto.EnemyId);
        var weaponDroprate = new WeaponDroprate
        {
            EnemyId = weaponDroprateRequestDto.EnemyId,
            WeaponId = weaponDroprateRequestDto.WeaponId,
            Droprate = weaponDroprateRequestDto.Droprate,
            Enemy = enemy,
            Weapon = weapon
        };
        await _weaponDroprateRepository.CreateAsync(weaponDroprate);
        return Created("agoraphobia/weaponDroprates", weaponDroprate.ToUpdateWeaponDroprateRequestDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromWeaponDroprate([FromRoute] int id)
    {
        var weaponDroprate = await _weaponDroprateRepository.GetByIdAsync(id);
        if (weaponDroprate is null)
            return NotFound();

        await _weaponDroprateRepository.DeleteAsync(id);
        return NoContent();
    }
}