using AgoraphobiaAPI.Dtos.ArmorDroprate;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.JoinTables.Armors;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[Route("agoraphobia/armorDroprates")]
[ApiController]
public class ArmorDroprateController : ControllerBase
{
    private readonly IEnemyRepository _enemyRepository;
    private readonly IArmorRepository _armorRepository;
    private readonly IArmorDroprateRepository _armorDroprateRepository;
    public ArmorDroprateController(
        IEnemyRepository enemyRepository,
        IArmorRepository armorRepository,
        IArmorDroprateRepository armorDroprateRepository
        )
    {
        _enemyRepository = enemyRepository;
        _armorRepository = armorRepository;
        _armorDroprateRepository = armorDroprateRepository;
    }

    [HttpGet("{enemyId}")]
    public async Task<IActionResult> GetArmorDroprate([FromRoute] int enemyId)
    {
        var enemy = await _enemyRepository.GetByIdAsync(enemyId);
        if (enemy is null)
            return NotFound();
        var armorDroprates = await _armorDroprateRepository.GetArmorDropratesAsync(enemyId);
        return Ok(armorDroprates.Select(x => x.ToArmorDroprateDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToArmorDroprate([FromBody] ArmorDroprateRequestDto armorDroprateRequestDto)
    {
        var enemy = await _enemyRepository.GetByIdAsync(armorDroprateRequestDto.EnemyId);
        var armor = await _armorRepository.GetByIdAsync(armorDroprateRequestDto.ArmorId);
        if (enemy is null)
            return BadRequest("Enemy not found");
        if (armor is null)
            return BadRequest("Armor not found");
        
        var armorDroprate = new ArmorDroprate
        {
            EnemyId = armorDroprateRequestDto.EnemyId,
            ArmorId = armorDroprateRequestDto.ArmorId,
            Droprate = armorDroprateRequestDto.Droprate,
            Armor = armor
        };
        await _armorDroprateRepository.CreateAsync(armorDroprate);
        return Created("agoraphobia/armorDroprates", armorDroprate.ToArmorDroprateDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromArmorDroprate([FromRoute] int id)
    {
        var armorDroprate = await _armorDroprateRepository.GetByIdAsync(id);
        if (armorDroprate is null)
            return NotFound();

        await _armorDroprateRepository.DeleteAsync(id);
        return NoContent();
    }
}