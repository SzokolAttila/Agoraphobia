using AgoraphobiaAPI.Dtos.Enemy;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/enemies")]
public class EnemyController : ControllerBase
{
    private readonly IEnemyRepository _enemyRepository;

    public EnemyController(IEnemyRepository enemyRepository)
    {
        _enemyRepository = enemyRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enemies = await _enemyRepository.GetAllAsync();
        return Ok(enemies.Select(x => x.ToEnemyDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var enemy = await _enemyRepository.GetByIdAsync(id);
        if (enemy is null)
            return NotFound();

        return Ok(enemy.ToEnemyDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnemyRequestDto enemyDto)
    {
        var enemy = enemyDto.ToEnemyFromCreateDto();
        var created = await _enemyRepository.CreateAsync(enemy);
        if (created is null)
            return BadRequest("Account not found");
        return CreatedAtAction(nameof(GetById), new { id = enemy.Id }, enemy.ToEnemyDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEnemyRequestDto enemyDto)
    {
        var enemy = await _enemyRepository.UpdateAsync(id, enemyDto);
        if (enemy is null)
            return NotFound();
        return Ok(enemy.ToEnemyDto());
    }

[HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var enemy = await _enemyRepository.DeleteAsync(id);
        if (enemy is null)
            return NotFound();
        return NoContent();
    }
}