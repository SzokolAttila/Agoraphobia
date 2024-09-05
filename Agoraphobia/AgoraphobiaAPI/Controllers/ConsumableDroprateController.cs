using AgoraphobiaAPI.Dtos.ConsumableDroprate;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[Route("agoraphobia/consumableDroprates")]
[ApiController]
public class ConsumableDroprateController : ControllerBase
{
    private readonly IEnemyRepository _enemyRepository;
    private readonly IConsumableRepository _consumableRepository;
    private readonly IConsumableDroprateRepository _consumableDroprateRepository;
    public ConsumableDroprateController(
        IEnemyRepository enemyRepository,
        IConsumableRepository consumableRepository,
        IConsumableDroprateRepository consumableDroprateRepository
        )
    {
        _enemyRepository = enemyRepository;
        _consumableRepository = consumableRepository;
        _consumableDroprateRepository = consumableDroprateRepository;
    }

    [HttpGet("{enemyId}")]
    public async Task<IActionResult> GetConsumableDroprate([FromRoute] int enemyId)
    {
        var enemy = await _enemyRepository.GetByIdAsync(enemyId);
        if (enemy is null)
            return NotFound();
        var consumableDroprates = await _consumableDroprateRepository.GetConsumableDropratesAsync(enemyId);
        return Ok(consumableDroprates.Select(x => x.ToConsumableDroprateDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToConsumableDroprate([FromBody] ConsumableDroprateRequestDto consumableDroprateRequestDto)
    {
        var enemy = await _enemyRepository.GetByIdAsync(consumableDroprateRequestDto.EnemyId);
        var consumable = await _consumableRepository.GetByIdAsync(consumableDroprateRequestDto.ConsumableId);
        if (enemy is null)
            return BadRequest("Enemy not found");
        if (consumable is null)
            return BadRequest("Consumable not found");
        
        var consumableDroprate = new ConsumableDroprate
        {
            EnemyId = consumableDroprateRequestDto.EnemyId,
            ConsumableId = consumableDroprateRequestDto.ConsumableId,
            Droprate = consumableDroprateRequestDto.Droprate,
            Consumable = consumable
        };
        await _consumableDroprateRepository.CreateAsync(consumableDroprate);
        return Created("agoraphobia/consumableDroprates", consumableDroprate.ToConsumableDroprateDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromConsumableDroprate([FromRoute] int id)
    {
        var consumableDroprate = await _consumableDroprateRepository.GetByIdAsync(id);
        if (consumableDroprate is null)
            return NotFound();

        await _consumableDroprateRepository.DeleteAsync(id);
        return NoContent();
    }
}